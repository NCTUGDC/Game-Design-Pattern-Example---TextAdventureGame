using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.Effectors.SkillEffectors;
using TextAdventureGame.Library.General.ItemElements;

namespace TextAdventureGame.Library.General
{
    public class BattleSystem
    {
        public struct SkillEffectStatus
        {
            public SustainSkillEffector effector;
            public int remainedRound;
        }
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public List<BattleFactors> MonsterBattleFactors { get; private set; }
        public List<SkillEffectStatus> PlayerSkillEffectStatuses { get; private set; }
        public List<List<SkillEffectStatus>> MonstersSkillEffectStatuses { get; private set; }

        private List<object> actionAgentList;

        public event Action OnStartTurn;
        public event Action OnPlayerActionRequest;
        public event Action OnEndTurn;
        public event Action OnEndBattle;
        public event Action OnRunSuccessiful;
        public event Action<bool, int> OnMiss;
        public event Action OnProcess;

        public BattleSystem(Player player, List<Monster> monsters)
        {
            Player = player;
            PlayerSkillEffectStatuses = new List<SkillEffectStatus>();

            Monsters = monsters;
            MonsterBattleFactors = new List<BattleFactors>();
            MonstersSkillEffectStatuses = new List<List<SkillEffectStatus>>();
            for(int i = 0; i < monsters.Count; i++)
            {
                MonsterBattleFactors.Add(BattleFactors.FromAbilityFactors(monsters[i].AbilityFactors));
                MonstersSkillEffectStatuses.Add(new List<SkillEffectStatus>());
            }
        }

        public void StartTurn()
        {
            actionAgentList = new List<object>();

            if(!PlayerSkillEffectStatuses.Any(x => x.effector is TargetStopActionSkillEffector))
                actionAgentList.Add(Player);
            for(int i = 0; i < Monsters.Count; i++)
            {
                if(!MonstersSkillEffectStatuses[i].Any(x => x.effector is TargetStopActionSkillEffector) && MonsterBattleFactors[i].healthPoint > 0)
                    actionAgentList.Add(i);
            }

            actionAgentList.OrderByDescending(x => 
            {
                if(x is Player)
                {
                    BattleFactors playerBF = Player.BattleFactors;
                    PlayerSkillEffectStatuses.ForEach(y => y.effector.Use(playerBF, null));
                    return playerBF.speedPoint;
                }
                else
                {
                    return MonsterBattleFactors[(int)x].speedPoint;
                }
            });
            if(MonsterBattleFactors.Any(x => x.healthPoint > 0))
            {
                OnStartTurn?.Invoke();
                ProcessTurn();
            }
            else
            {
                EndBattle();
            }
        }
        public void ProcessTurn()
        {
            OnProcess?.Invoke();
            if (actionAgentList.Count > 0)
            {
                if(actionAgentList[0] == Player)
                {
                    OnPlayerActionRequest?.Invoke();
                    actionAgentList.Remove(Player);
                }
                else
                {
                    int enemyIndex = (int)actionAgentList[0];
                    
                    BattleFactors monsterBF = MonsterBattleFactors[enemyIndex];
                    BattleFactors playerBF = Player.BattleFactors;
                    PlayerSkillEffectStatuses.ForEach(x => x.effector.Use(playerBF, null));
                    if (HitCheck(monsterBF, playerBF))
                    {
                        Skill skill = Monsters[enemyIndex].Action();
                        if (skill == null)
                        {
                            Player.AbilityFactors.HP -= Math.Max(monsterBF.physicalAttackPoint - playerBF.physicalDefencePoint, 1);
                        }
                        else
                        {
                            skill.Use(monsterBF, new List<BattleFactors> { playerBF });
                            skill.SkillEffectors.OfType<SustainSkillEffector>().ToList().ForEach(x =>
                            {
                                SkillEffectStatus status = new SkillEffectStatus { effector = x, remainedRound = x.SustainRound };
                                if (x is TargetSpeedPointSkillEffector || x is TargetStopActionSkillEffector)
                                {
                                    PlayerSkillEffectStatuses.Add(status);
                                }
                                else
                                {
                                    MonstersSkillEffectStatuses[enemyIndex].Add(status);
                                }
                            });
                        }
                    }
                    else
                    {
                        OnMiss?.Invoke(true, enemyIndex);
                    }
                    ProcessTurn();
                }
            }
            else
            {
                EndTurn();
            }
        }
        public void EndTurn()
        {
            PlayerSkillEffectStatuses.ForEach(x => x.remainedRound--);
            PlayerSkillEffectStatuses.RemoveAll(x => x.remainedRound < 0);
            for(int i = 0; i < Monsters.Count; i++)
            {
                MonstersSkillEffectStatuses[i].ForEach(effector => effector.remainedRound--);
                MonstersSkillEffectStatuses[i].RemoveAll(effector =>
                {
                    effector.effector.End(MonsterBattleFactors[i]);
                    return effector.remainedRound < 0;
                });
            }
            OnEndTurn?.Invoke();
            StartTurn();
        }
        public void EndBattle()
        {
            Player.EXP += Monsters.Sum(x => x.EXP);
            OnEndBattle?.Invoke();
        }

        public void NormalAttack(int enemyIndex)
        {
            BattleFactors enemyBF = MonsterBattleFactors[enemyIndex];
            BattleFactors playerBF = Player.BattleFactors;
            PlayerSkillEffectStatuses.ForEach(x => x.effector.Use(playerBF, null));
            if (HitCheck(playerBF, enemyBF))
            {
                enemyBF.healthPoint -= Math.Max(playerBF.physicalAttackPoint - enemyBF.physicalDefencePoint, 1);
                if(enemyBF.healthPoint <= 0 && actionAgentList.Any(x => x is int && (int)x == enemyIndex))
                {
                    actionAgentList.RemoveAll(x => x is int && (int)x == enemyIndex);
                }
            }
            else
            {
                OnMiss?.Invoke(false, 0);
            }
            ProcessTurn();
        }
        public void UseSkill(int skillID, int enemyIndex)
        {
            if(Player.HasSkill(skillID))
            {
                Skill skill = SkillFactory.Instance.FindSkill(skillID);
                BattleFactors playerBF = Player.BattleFactors;
                PlayerSkillEffectStatuses.ForEach(x => x.effector.Use(playerBF, null));
                if (skill.IsAoE)
                {
                    var affectedTargets = MonsterBattleFactors.Where(x => x.healthPoint >= 0 && HitCheck(playerBF, x)).ToList();
                    skill.Use(playerBF, affectedTargets);
                    skill.SkillEffectors.OfType<SustainSkillEffector>().ToList().ForEach(effector => 
                    {
                        SkillEffectStatus status = new SkillEffectStatus { effector = effector, remainedRound = effector.SustainRound };
                        if (effector is TargetSpeedPointSkillEffector || effector is TargetStopActionSkillEffector)
                        {
                            foreach(var affectedTarget in affectedTargets)
                            {
                                MonstersSkillEffectStatuses[MonsterBattleFactors.IndexOf(affectedTarget)].Add(status);
                            }
                        }
                        else
                        {
                            PlayerSkillEffectStatuses.Add(status);
                        }
                    });
                }
                else
                {
                    if (HitCheck(playerBF, MonsterBattleFactors[enemyIndex]))
                    {
                        skill.Use(playerBF, new List<BattleFactors> { MonsterBattleFactors[enemyIndex] });
                        skill.SkillEffectors.OfType<SustainSkillEffector>().ToList().ForEach(x =>
                        {
                            SkillEffectStatus status = new SkillEffectStatus { effector = x, remainedRound = x.SustainRound };
                            if (x is TargetSpeedPointSkillEffector || x is TargetStopActionSkillEffector)
                            {
                                MonstersSkillEffectStatuses[enemyIndex].Add(status);
                            }
                            else
                            {
                                PlayerSkillEffectStatuses.Add(status);
                            }
                        });
                    }
                    else
                    {
                        OnMiss?.Invoke(false, 0);
                    }
                }
            }
            ProcessTurn();
        }
        public void UseItem(int itemID)
        {
            if(Player.Inventory.ItemCount(itemID) > 0 && ItemFactory.Instance.FindItem(itemID) is Consumable)
            {
                Player.Inventory.RemoveItem(itemID, 1);
                (ItemFactory.Instance.FindItem(itemID) as Consumable).Use(Player.AbilityFactors);
            }
            ProcessTurn();
        }
        public void Run()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            if(randomGenerator.NextDouble() < 0.6)
            {
                OnRunSuccessiful?.Invoke();
            }
            else
            {
                ProcessTurn();
            }
        }

        private bool HitCheck(BattleFactors caster, BattleFactors target)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            return randomGenerator.Next(0, caster.accuracyPoint + target.evasionPoint) < caster.accuracyPoint;
        }
    }
}

using System;
using System.Collections.Generic;
using TextAdventureGame.Library.General.ItemElements;
using System.Linq;

namespace TextAdventureGame.Library.General
{
    public class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                onNameChange?.Invoke(name);
            }
        }

        private int locatedSceneID;
        public int LocatedSceneID
        {
            get { return locatedSceneID; }
            set
            {
                locatedSceneID = value;
                onLocatedSceneIDChange?.Invoke(locatedSceneID);
            }
        }
        public Inventory Inventory { get; private set; }
        public AbilityFactors AbilityFactors { get; private set; }
        public Equipment Weapon { get; set; }
        public Equipment HeadEquipment { get; set; }
        public Equipment BodyEquipment { get; set; }
        public Equipment FootEquipment { get; set; }
        public Equipment Accessory { get; set; }

        private int levelUpEXP;
        public int LevelUpEXP
        {
            get { return levelUpEXP; }
            set
            {
                levelUpEXP = value;
                exp = 0;
                onLevelUpEXPChange?.Invoke(levelUpEXP);
            }
        }

        private int exp;
        public int EXP
        {
            get { return exp; }
            set
            {
                int totalEXP = value;
                while(totalEXP > LevelUpEXP)
                {
                    totalEXP -= LevelUpEXP;
                    onEXP_Change?.Invoke(LevelUpEXP);
                }
                exp = Math.Max(totalEXP, 0);
                onEXP_Change?.Invoke(exp);
            }
        }

        private int abilityPoint;
        public int AbilityPoint
        {
            get { return abilityPoint; }
            set
            {
                abilityPoint = Math.Max(value, 0);
                onAbilityPointChange?.Invoke(abilityPoint);
            }
        }
        public BattleFactors BattleFactors
        {
            get
            {
                BattleFactors bf = BattleFactors.FromAbilityFactors(AbilityFactors);
                if (HeadEquipment != null)
                    bf = HeadEquipment.Use(bf);
                if (BodyEquipment != null)
                    bf = BodyEquipment.Use(bf);
                if (FootEquipment != null)
                    bf = FootEquipment.Use(bf);
                if (Weapon != null)
                    bf = Weapon.Use(bf);
                if (Accessory != null)
                    bf = Accessory.Use(bf);
                return bf;
            }
        }

        private List<int> skills;
        public IEnumerable<int> Skills { get { return skills; } }

        private event Action<string> onNameChange;
        public event Action<string> OnNameChange { add { onNameChange += value; } remove { onNameChange -= value; } }

        private event Action<int> onLocatedSceneIDChange;
        public event Action<int> OnLocatedSceneIDChange { add { onLocatedSceneIDChange += value; } remove { onLocatedSceneIDChange -= value; } }

        private event Action<int> onLevelUpEXPChange;
        public event Action<int> OnLevelUpEXPChange { add { onLevelUpEXPChange += value; } remove { onLevelUpEXPChange -= value; } }

        private event Action<int> onEXP_Change;
        public event Action<int> OnEXP_Change { add { onEXP_Change += value; } remove { onEXP_Change -= value; } }

        private event Action<int> onAbilityPointChange;
        public event Action<int> OnAbilityPointChange { add { onAbilityPointChange += value; } remove { onAbilityPointChange -= value; } }

        private event Action<int> onLearnSkill;
        public event Action<int> OnLearnSkill { add { onLearnSkill += value; } remove { onLearnSkill -= value; } }

        public Player()
        {
            Inventory = new Inventory();
            AbilityFactors = new AbilityFactors
            {
                Level = 1,
                MaxHP = 50,
                HP = 50,
                MaxSP = 25,
                SP = 25,
                Power = 1,
                Magic = 1,
                Agile = 1,
                Sensibility = 1
            };
            LevelUpEXP = LevelEXPTable.GetLevelUpEXP(1);
            EXP = 0;
            AbilityPoint = 6;
            skills = new List<int>();
            locatedSceneID = 1;

            Action learnSkillAction = () => 
            {
                var remaindedSkills = SkillFactory.Instance.Skills.Where(x => !HasSkill(x.SkillID));
                foreach (var skill in remaindedSkills)
                {
                    if (skill.CanLearn(this))
                    {
                        LearnSkill(skill.SkillID);
                    }
                }
            };

            AbilityFactors.OnLevelChange += (value1, value2) => learnSkillAction();
            AbilityFactors.OnPowerChange += (value) => learnSkillAction();
            AbilityFactors.OnMagicChange += (value) => learnSkillAction();
            AbilityFactors.OnAgileChange += (value) => learnSkillAction();
            AbilityFactors.OnSensibilityChange += (value) => learnSkillAction();
        }

        public bool HasSkill(int skillID)
        {
            return skills.Contains(skillID);
        }
        public void LearnSkill(int skillID)
        {
            if(!HasSkill(skillID))
            {
                skills.Add(skillID);
                onLearnSkill?.Invoke(skillID);
            }
        }
    }
}

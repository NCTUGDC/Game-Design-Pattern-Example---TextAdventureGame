using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.Effectors;

namespace TextAdventureGame.Library.General
{
    public class Skill
    {
        [MessagePackMember(id: 0, Name = "SkillID")]
        public int SkillID { get; private set; }
        [MessagePackMember(id: 1, Name = "SkillName")]
        public string SkillName { get; private set; }
        [MessagePackMember(id: 2, Name = "RequiredSP")]
        public int RequiredSP { get; private set; }
        [MessagePackMember(id: 3, Name = "IsAoE")]
        public bool IsAoE { get; private set; }

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 4, Name = "abilityCoditionEffectors")]
        protected List<AbilityConditionEffector> abilityCoditionEffectors;
        public IEnumerable<AbilityConditionEffector> AbilityConditionEffectors { get { return abilityCoditionEffectors; } }

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 5, Name = "skillEffectors")]
        protected List<SkillEffector> skillEffectors;
        public IEnumerable<SkillEffector> SkillEffectors { get { return skillEffectors; } }

        [MessagePackDeserializationConstructor]
        public Skill() { }
        public Skill(int skillID, string skillName, int requiredSP, bool isAoE)
        {
            SkillID = skillID;
            SkillName = skillName;
            RequiredSP = requiredSP;
            IsAoE = isAoE;
            abilityCoditionEffectors = new List<AbilityConditionEffector>();
            skillEffectors = new List<SkillEffector>();
        }
        public bool CanLearn(Player player)
        {
            if (abilityCoditionEffectors.Count != 0)
            {
                return abilityCoditionEffectors.All(x => x.IsSufficient(player.AbilityFactors));
            }
            else
            {
                return true;
            }
        }
        public void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.skillPoint -= RequiredSP;
            foreach (var skillEffector in skillEffectors)
            {
                skillEffector.Use(casterFactors, targetsFactors);
            }
        }
        public void AddAbilityConditionEffector(AbilityConditionEffector abilityConditionEffector)
        {
            abilityCoditionEffectors.Add(abilityConditionEffector);
        }
        public void RemoveAbilityConditionEffector(AbilityConditionEffector abilityConditionEffector)
        {
            abilityCoditionEffectors.Remove(abilityConditionEffector);
        }
        public void AddSkillEffector(SkillEffector skillEffector)
        {
            skillEffectors.Add(skillEffector);
        }
        public void RemoveSkillEffector(SkillEffector skillEffector)
        {
            skillEffectors.Remove(skillEffector);
        }
    }
}

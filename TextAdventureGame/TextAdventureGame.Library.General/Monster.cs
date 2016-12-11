using MsgPack.Serialization;
using System.Collections.Generic;
using System;

namespace TextAdventureGame.Library.General
{
    public class Monster
    {
        [MessagePackMember(id: 0, Name = "MonsterID")]
        public int MonsterID { get; private set; }
        [MessagePackMember(id: 1, Name = "MonsterName")]
        public string MonsterName { get; private set; }
        [MessagePackMember(id: 2, Name = "AbilityFactors")]
        public AbilityFactors AbilityFactors { get; private set; }
        [MessagePackMember(id: 3, Name = "EXP")]
        public int EXP { get; private set; }

        [MessagePackMember(id: 4, Name = "skills")]
        private List<int> skills;
        public IEnumerable<int> Skills { get { return skills; } }

        [MessagePackMember(id: 5, Name = "skillProbabilityDictionary")]
        private Dictionary<int, int> skillProbabilityDictionary;

        [MessagePackDeserializationConstructor]
        public Monster() { }
        public Monster(int monsterID, string monsterName, AbilityFactors abilityFactors, int exp, List<int> skills, Dictionary<int, int> skillProbabilityDictionary)
        {
            MonsterID = monsterID;
            MonsterName = monsterName;
            AbilityFactors = abilityFactors;
            EXP = exp;
            this.skills = skills;
            this.skillProbabilityDictionary = skillProbabilityDictionary;
        }

        public Skill Action()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            int number = randomGenerator.Next(1, 101);
            for(int i = 0; i < skills.Count; i++)
            {
                int skillID = skills[i];
                if(skillProbabilityDictionary.ContainsKey(skillID) && skillProbabilityDictionary[skillID] <= number)
                {
                    return SkillFactory.Instance.FindSkill(skillID);
                }
            }
            return null;
        }
    }
}

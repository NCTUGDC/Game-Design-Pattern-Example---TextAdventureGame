using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class SkillFactory
    {
        private static SkillFactory instance;
        public static SkillFactory Instance { get { return instance; } }
        public static void InitialFactory(SkillFactory factory)
        {
            instance = factory;
        }
        public static SkillFactory LoadFactory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<SkillFactory>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveFactory(string fileName, SkillFactory factory)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(factory));
        }
        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 0, Name = "skillDictionary")]
        private Dictionary<int, Skill> skillDictionary;
        public IEnumerable<Skill> Skills { get { return skillDictionary.Values; } }
        public int SkillCount { get { return skillDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public SkillFactory()
        {
            skillDictionary = new Dictionary<int, Skill>();
        }
        public bool ContainsSkill(int skillID)
        {
            return skillDictionary.ContainsKey(skillID);
        }
        public Skill FindSkill(int skillID)
        {
            if (ContainsSkill(skillID))
            {
                return skillDictionary[skillID];
            }
            else
            {
                return null;
            }
        }
        public void AddSkill(Skill skill)
        {
            if (!ContainsSkill(skill.SkillID))
            {
                skillDictionary.Add(skill.SkillID, skill);
            }
        }
        public bool RemoveSkill(int skillID)
        {
            return skillDictionary.Remove(skillID);
        }
    }
}

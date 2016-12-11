using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class MonsterFactory
    {
        private static MonsterFactory instance;
        public static MonsterFactory Instance { get { return instance; } }
        public static void InitialFactory(MonsterFactory factory)
        {
            instance = factory;
        }
        public static MonsterFactory LoadFactory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<MonsterFactory>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveFactory(string fileName, MonsterFactory factory)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(factory));
        }
        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 0, Name = "monsterDictionary")]
        private Dictionary<int, Monster> monsterDictionary;
        public IEnumerable<Monster> Monsters { get { return monsterDictionary.Values; } }
        public int MonsterCount { get { return monsterDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public MonsterFactory()
        {
            monsterDictionary = new Dictionary<int, Monster>();
        }
        public bool ContainsMonster(int monsterID)
        {
            return monsterDictionary.ContainsKey(monsterID);
        }
        public Monster FindMonster(int monsterID)
        {
            if (ContainsMonster(monsterID))
            {
                return monsterDictionary[monsterID];
            }
            else
            {
                return null;
            }
        }
        public void AddMonster(Monster monster)
        {
            if (!ContainsMonster(monster.MonsterID))
            {
                monsterDictionary.Add(monster.MonsterID, monster);
            }
        }
        public bool RemoveMonster(int monsterID)
        {
            return monsterDictionary.Remove(monsterID);
        }
    }
}

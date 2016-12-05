using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class NPC_Factory
    {
        private static NPC_Factory instance;
        public static NPC_Factory Instance { get { return instance; } }
        public static void InitialFactory(NPC_Factory factory)
        {
            instance = factory;
        }
        public static NPC_Factory LoadFactory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<NPC_Factory>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveFactory(string fileName, NPC_Factory factory)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(factory));
        }

        [MessagePackRuntimeDictionaryKeyType]
        [MessagePackMember(id: 0, Name = "npcDictionary")]
        private Dictionary<int, NPC> npcDictionary;
        public IEnumerable<NPC> NPCs { get { return npcDictionary.Values; } }
        public int NPC_Count { get { return npcDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public NPC_Factory()
        {
            npcDictionary = new Dictionary<int, NPC>();
        }
        public bool ContainsNPC(int npcID)
        {
            return npcDictionary.ContainsKey(npcID);
        }
        public NPC FindNPC(int npcID)
        {
            if (ContainsNPC(npcID))
            {
                return npcDictionary[npcID];
            }
            else
            {
                return null;
            }
        }
        public void AddNPC(NPC npc)
        {
            if (!ContainsNPC(npc.NPC_ID))
            {
                npcDictionary.Add(npc.NPC_ID, npc);
            }
        }
        public bool RemoveNPC(int npcID)
        {
            return npcDictionary.Remove(npcID);
        }
    }
}

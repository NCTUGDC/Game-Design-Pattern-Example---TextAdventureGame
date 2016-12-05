using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class StoreFactory
    {
        private static StoreFactory instance;
        public static StoreFactory Instance { get { return instance; } }
        public static void InitialFactory(StoreFactory factory)
        {
            instance = factory;
        }
        public static StoreFactory LoadFactory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<StoreFactory>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveFactory(string fileName, StoreFactory factory)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(factory));
        }

        [MessagePackRuntimeDictionaryKeyType]
        [MessagePackMember(id: 0, Name = "storeDictionary")]
        private Dictionary<int, Store> storeDictionary;
        public IEnumerable<Store> Stores { get { return storeDictionary.Values; } }
        public int StoreCount { get { return storeDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public StoreFactory()
        {
            storeDictionary = new Dictionary<int, Store>();
        }
        public bool ContainsStore(int storeID)
        {
            return storeDictionary.ContainsKey(storeID);
        }
        public Store FindStore(int storeID)
        {
            if (ContainsStore(storeID))
            {
                return storeDictionary[storeID];
            }
            else
            {
                return null;
            }
        }
        public void AddStore(Store store)
        {
            if (!ContainsStore(store.StoreID))
            {
                storeDictionary.Add(store.StoreID, store);
            }
        }
        public bool RemoveStore(int storeID)
        {
            return storeDictionary.Remove(storeID);
        }
    }
}

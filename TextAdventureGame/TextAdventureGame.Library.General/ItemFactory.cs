using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class ItemFactory
    {
        private static ItemFactory instance;
        public static ItemFactory Instance { get { return instance; } }
        public static void InitialFactory(ItemFactory factory)
        {
            instance = factory;
        }
        public static ItemFactory LoadFactory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<ItemFactory>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveFactory(string fileName, ItemFactory factory)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(factory));
        }
        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 0, Name = "itemDictionary")]
        private Dictionary<int, Item> itemDictionary;
        public IEnumerable<Item> Items { get { return itemDictionary.Values; } }
        public int ItemCount { get { return itemDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public ItemFactory()
        {
            itemDictionary = new Dictionary<int, Item>();
        }
        public bool ContainsItem(int itemID)
        {
            return itemDictionary.ContainsKey(itemID);
        }
        public Item FindItem(int itemID)
        {
            if (ContainsItem(itemID))
            {
                return itemDictionary[itemID];
            }
            else
            {
                return null;
            }
        }
        public void AddItem(Item item)
        {
            if (!ContainsItem(item.ItemID))
            {
                itemDictionary.Add(item.ItemID, item);
            }
        }
        public bool RemoveItem(int itemID)
        {
            return itemDictionary.Remove(itemID);
        }
    }
}

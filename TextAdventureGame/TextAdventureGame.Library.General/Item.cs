using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.ItemElements;

namespace TextAdventureGame.Library.General
{
    public class Item
    {
        [MessagePackMember(id: 0, Name = "ItemID")]
        public int ItemID { get; private set; }
        [MessagePackMember(id: 1, Name = "ItemName")]
        public string ItemName { get; private set; }
        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 2, Name = "components")]
        private List<ItemComponent> components;
        public IEnumerable<ItemComponent> Components { get { return components; } }

        [MessagePackDeserializationConstructor]
        public Item() { }

        public Item(int itemID, string itemName)
        {
            ItemID = itemID;
            ItemName = itemName;
            components = new List<ItemComponent>();
        }
        public bool Use(ItemComponentTypeCode useType, List<object> targets)
        {
            if (components.Count != 0)
            {
                return components.Where(x => x.ItemComponentTypeCode == useType).All(x => x.Use(targets));
            }
            else
            {
                return false;
            }
        }
        public void AddComponent(ItemComponent component)
        {
            components.Add(component);
        }
        public void RemoveComponent(ItemComponent component)
        {
            components.Remove(component);
        }
    }
}

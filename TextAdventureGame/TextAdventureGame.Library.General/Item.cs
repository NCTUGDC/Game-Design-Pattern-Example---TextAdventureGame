using MsgPack.Serialization;

namespace TextAdventureGame.Library.General
{
    public class Item
    {
        [MessagePackMember(id: 0, Name = "ItemID")]
        public int ItemID { get; private set; }
        [MessagePackMember(id: 1, Name = "ItemName")]
        public string ItemName { get; private set; }

        [MessagePackDeserializationConstructor]
        public Item() { }

        public Item(int itemID, string itemName)
        {
            ItemID = itemID;
            ItemName = itemName;
        }
    }
}

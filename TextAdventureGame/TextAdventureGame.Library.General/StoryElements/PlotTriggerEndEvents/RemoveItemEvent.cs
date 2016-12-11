using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class RemoveItemEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "ItemID")]
        public int ItemID { get; private set; }
        [MessagePackMember(id: 2, Name = "itemID")]
        public int ItemCount { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("失去物品 ID： {0}", ItemID);
            }
        }

        public RemoveItemEvent(int endEventID, int itemID, int itemCount) : base(endEventID)
        {
            ItemID = itemID;
            ItemCount = itemCount;
        }

        public override void Execute()
        {
            PlayerManager.Instance.Player.Inventory.RemoveItem(ItemID, ItemCount);
        }
    }
}

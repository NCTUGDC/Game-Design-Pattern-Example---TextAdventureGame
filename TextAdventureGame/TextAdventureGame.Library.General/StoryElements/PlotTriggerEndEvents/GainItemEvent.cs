﻿using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class GainItemEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "ItemID")]
        public int ItemID { get; private set; }
        [MessagePackMember(id: 2, Name = "ItemCount")]
        public int ItemCount { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("獲得物品 ID： {0}, Count: {1}", ItemID, ItemCount);
            }
        }

        public GainItemEvent() { }
        public GainItemEvent(int endEventID, int itemID, int itemCount) : base(endEventID)
        {
            ItemID = itemID;
            ItemCount = itemCount;
        }

        public override void Execute()
        {
            PlayerManager.Instance.Player.Inventory.AddItem(ItemFactory.Instance.FindItem(ItemID), ItemCount);
        }
    }
}

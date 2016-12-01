using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerConditions
{
    public class HavingItemCondition : PlotTriggerCondition
    {
        [MessagePackMember(id: 1, Name = "ItemID")]
        public int ItemID { get; private set; }
        [MessagePackMember(id: 2, Name = "ItemCount")]
        public int ItemCount { get; private set; }

        public override string ConditionInformation
        {
            get
            {
                return string.Format("擁有物品 ItemID: {0}, 數量: {1}", ItemID, ItemCount);
            }
        }

        public HavingItemCondition(int conditionID, int itemID, int itemCount) : base(conditionID)
        {
            ItemID = itemID;
            ItemCount = itemCount;
        }

        public override bool IsEligible()
        {
            return PlayerManager.Instance.Player.Inventory.ItemCount(ItemID) >= ItemCount;
        }
    }
}

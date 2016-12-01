namespace TextAdventureGame.Library.General.ItemElements
{
    public class InventoryItemInfo
    {
        public int ItemID { get; set; }
        public int Count { get; set; }

        public InventoryItemInfo(int itemID, int count)
        {
            ItemID = itemID;
            Count = count;
        }
    }
}

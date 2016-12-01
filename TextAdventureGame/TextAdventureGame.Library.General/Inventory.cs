using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.ItemElements;

namespace TextAdventureGame.Library.General
{
    public class Inventory
    {
        protected Dictionary<int, InventoryItemInfo> itemInfoDictionary;
        public int DifferentItemCount { get { return itemInfoDictionary.Count; } }


        public IEnumerable<InventoryItemInfo> ItemInfos { get { return itemInfoDictionary.Values; } }

        private event Action<InventoryItemInfo> onItemChange;
        public event Action<InventoryItemInfo> OnItemChange { add { onItemChange += value; } remove { onItemChange -= value; } }

        public Inventory()
        {
            itemInfoDictionary = new Dictionary<int, InventoryItemInfo>();
        }
        public bool ContainsItem(int itemID)
        {
            return itemInfoDictionary.Values.Any(x => x.ItemID == itemID);
        }
        public InventoryItemInfo FindInventoryItemInfo(int itemID)
        {
            if (ContainsItem(itemID))
            {
                return ItemInfos.First(x => x.ItemID == itemID);
            }
            else
            {
                return null;
            }
        }
        public int ItemCount(int itemID)
        {
            if (ContainsItem(itemID))
            {
                return ItemInfos.First(x => x.ItemID == itemID).Count;
            }
            else
            {
                return 0;
            }
        }
        public bool AddItem(Item item, int count)
        {
            InventoryItemInfo info = FindInventoryItemInfo(item.ItemID);
            if (info == null)
            {
                itemInfoDictionary.Add(info.ItemID, info);
            }
            else
            {
                info.Count += count;
            }
            onItemChange?.Invoke(info);
            return true;
        }
        public bool RemoveItem(int itemID, int count)
        {
            if (ContainsItem(itemID) && ItemCount(itemID) >= count)
            {
                InventoryItemInfo info = FindInventoryItemInfo(itemID);
                info.Count -= count;
                if (info.Count == 0)
                {
                    if (itemInfoDictionary.ContainsKey(info.ItemID))
                    {
                        itemInfoDictionary.Remove(info.ItemID);
                    }
                }
                onItemChange?.Invoke(info);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

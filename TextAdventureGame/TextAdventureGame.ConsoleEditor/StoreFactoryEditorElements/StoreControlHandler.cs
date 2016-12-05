using System;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.StoreElements;

namespace TextAdventureGame.ConsoleEditor.StoreFactoryEditorElements
{
    public class StoreControlHandler : EditorControlHandler
    {
        private Store editingStore;

        public override string ControlInformation
        {
            get
            {
                return "商店編輯器： 輸入 help 了解操作方式";
            }
        }

        public StoreControlHandler(Store store)
        {
            editingStore = store;
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "back to store factory":
                        BackToStoreFactoryCommandTask(out rollbackLayerCount);
                        break;
                    case "add trade info":
                        AddTradeInfomationCommandTask();
                        break;
                    case "remove trade info":
                        RemoveTradeInfomationCommandTask();
                        break;
                    default:
                        canHandle = false;
                        break;
                }
                return canHandle;
            }
            else
            {
                return true;
            }
        }

        #region command tasks
        protected override void HelpCommandTask()
        {
            base.HelpCommandTask();
            Console.WriteLine("\t輸入add trade info加入新交易項目");
            Console.WriteLine("\t輸入remove trade info移除交易項目");
            Console.WriteLine("\t輸入back to store factory返回商店工廠層級");
        }
        private void BackToStoreFactoryCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        protected override void ViewCommandTask()
        {
            var tradeInformations = editingStore.TradeInformations;
            Console.WriteLine("商店ID: {0} 名稱: {1}, 共有{2}筆交易項目", editingStore.StoreID, editingStore.StoreName, tradeInformations.Count);
            Console.WriteLine("交易項目:");
            for(int i = 0; i < tradeInformations.Count; i++)
            {
                Console.WriteLine("\tIndex: {0}", i);
                Console.Write("\t\t消耗: ");
                foreach(var cost in tradeInformations[i].Costs)
                {
                    Console.Write("\t物品ID: {0}, 數量: {1} ", cost.itemID, cost.count);
                }
                Console.WriteLine();
                Console.Write("\t\t取得: ");
                foreach (var reward in tradeInformations[i].Rewards)
                {
                    Console.Write("\t物品ID: {0}, 數量: {1} ", reward.itemID, reward.count);
                }
                Console.WriteLine();
            }
        }
        private void AddTradeInfomationCommandTask()
        {
            TradeInformation tradeInformation = new TradeInformation();
            string inputString;
            do
            {
                int itemID = 0;
                int count = 0;
                Console.Write("請輸入消耗物品 ID(輸入end結束): ");
                inputString = Console.ReadLine();
                while (inputString != "end" && (!int.TryParse(inputString, out itemID)))
                {
                    Console.Write("不合法的輸入 請輸入消耗物品 ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "end")
                {
                    Console.Write("請輸入消耗物品 數量(輸入end結束): ");
                    inputString = Console.ReadLine();
                    while (inputString != "end" && (!int.TryParse(inputString, out count)))
                    {
                        Console.Write("不合法的輸入 請輸入消耗物品 數量(整數)(輸入cancel取消): ");
                        inputString = Console.ReadLine();
                    }
                    if (inputString != "end")
                    {
                        tradeInformation.AddCost(new TradeItemInformation { itemID = itemID, count = count });
                    }
                }
            }
            while (inputString != "end");
            do
            {
                int itemID = 0;
                int count = 0;
                Console.Write("請輸入取得物品 ID(輸入end結束): ");
                inputString = Console.ReadLine();
                while (inputString != "end" && (!int.TryParse(inputString, out itemID)))
                {
                    Console.Write("不合法的輸入 請輸入取得物品 ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "end")
                {
                    Console.Write("請輸入取得物品 數量(輸入end結束): ");
                    inputString = Console.ReadLine();
                    while (inputString != "end" && (!int.TryParse(inputString, out count)))
                    {
                        Console.Write("不合法的輸入 請輸入取得物品 數量(整數)(輸入cancel取消): ");
                        inputString = Console.ReadLine();
                    }
                    if (inputString != "end")
                    {
                        tradeInformation.AddReward(new TradeItemInformation { itemID = itemID, count = count });
                    }
                }
            }
            while (inputString != "end");
            editingStore.AddTradeInformation(tradeInformation);
            ViewCommandTask();
        }
        private void RemoveTradeInfomationCommandTask()
        {
            Console.Write("請輸入要刪除的交易項目索引號(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int index = 0;
            while (inputString != "cancel" && (!int.TryParse(inputString, out index) || index <= 0 || index > editingStore.TradeInformations.Count))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的交易項目索引號(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                editingStore.RemoveTradeInformationAt(index);
                Console.WriteLine("已刪除交易項目");
            }
        }
        #endregion
    }
}

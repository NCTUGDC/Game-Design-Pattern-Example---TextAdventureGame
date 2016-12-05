using System;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.ConsoleEditor.StoreFactoryEditorElements
{
    public class StoreFactoryControlHandler : EditorControlHandler
    {
        private StoreFactory editingFactory;

        public override string ControlInformation
        {
            get
            {
                return "商店工廠編輯器： 輸入 help 了解操作方式";
            }
        }

        public StoreFactoryControlHandler(StoreFactory factory)
        {
            editingFactory = factory;
        }
        private void SaveFactory(string filePath)
        {
            StoreFactory.SaveFactory(filePath, editingFactory);
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "exit":
                        ExitCommandTask(out rollbackLayerCount);
                        break;
                    case "save":
                        SaveCommandTask();
                        break;
                    case "add store":
                        AddStoreCommandTask();
                        break;
                    case "load store":
                        LoadStoreCommandTask();
                        break;
                    case "remove store":
                        RemoveStoreCommandTask();
                        break;
                    case "clear":
                        ClearCommandTask();
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
            Console.WriteLine("\t輸入exit離開商店工廠編輯器");
            Console.WriteLine("\t輸入save儲存商店工廠(名稱先不用輸入)");
            Console.WriteLine("\t輸入view檢視商店工廠資訊");
            Console.WriteLine("\t輸入add store加入新商店");
            Console.WriteLine("\t輸入load store載入商店");
            Console.WriteLine("\t輸入remove store移除商店");
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void SaveCommandTask()
        {
            Console.Write("請輸入要儲存的檔案路徑與名稱: ");
            SaveFactory(Console.ReadLine());
            Console.WriteLine("儲存成功!");
        }
        protected override void ViewCommandTask()
        {
            Console.WriteLine("共有{0}間商店", editingFactory.StoreCount);
            foreach (var store in editingFactory.Stores)
            {
                Console.WriteLine("\t商店 ID: {0} 名稱： {1}, 商品數量： {2}", store.StoreID, store.StoreName, store.TradeInformations.Count);
            }
        }
        private void AddStoreCommandTask()
        {
            Console.Write("請輸入新商店 ID(輸入cancel取消): ");
            int storeID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out storeID) || editingFactory.ContainsStore(storeID)))
                {
                    if (editingFactory.ContainsStore(storeID))
                    {
                        Console.Write("ID已存在 請輸入新商店 ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新商店 ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入商店名稱: ");
                    string storeName = Console.ReadLine();
                    editingFactory.AddStore(new Store(storeID, storeName));
                    ViewCommandTask();
                }
            }
        }
        private void LoadStoreCommandTask()
        {
            Console.Write("請輸入要讀取的商店ID(輸入cancel取消): ");
            int storeID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out storeID) || !editingFactory.ContainsStore(storeID)))
                {
                    if (!editingFactory.ContainsStore(storeID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的商店ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的商店ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new StoreControlHandler(editingFactory.FindStore(storeID));
                }
            }
        }
        private void RemoveStoreCommandTask()
        {
            Console.Write("請輸入要刪除的商店 ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int storeID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out storeID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的商店 ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if (editingFactory.RemoveStore(storeID))
                {
                    Console.WriteLine("已刪除商店");
                }
                else
                {
                    Console.WriteLine("刪除失敗");
                }
            }
        }
        #endregion
    }
}

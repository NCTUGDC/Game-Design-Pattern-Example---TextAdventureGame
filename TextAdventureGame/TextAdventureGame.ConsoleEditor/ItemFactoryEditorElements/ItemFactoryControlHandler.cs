using System;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.ConsoleEditor.ItemFactoryEditorElements
{
    public class ItemFactoryControlHandler : EditorControlHandler
    {
        private ItemFactory editingFactory;

        public override string ControlInformation
        {
            get
            {
                return "物品工廠編輯器： 輸入 help 了解操作方式";
            }
        }

        public ItemFactoryControlHandler(ItemFactory factory)
        {
            editingFactory = factory;
        }
        private void SaveFactory(string filePath)
        {
            ItemFactory.SaveFactory(filePath, editingFactory);
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
                    case "add item":
                        AddItemCommandTask();
                        break;
                    case "remove item":
                        RemoveItemCommandTask();
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
            Console.WriteLine("\t輸入exit離開物品工廠編輯器");
            Console.WriteLine("\t輸入save儲存物品工廠(名稱先不用輸入)");
            Console.WriteLine("\t輸入view檢視物品工廠資訊");
            Console.WriteLine("\t輸入add item加入新物品");
            Console.WriteLine("\t輸入remove item移除物品");
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
            Console.WriteLine("共有{0}個物品", editingFactory.ItemCount);
            foreach (var item in editingFactory.Items)
            {
                Console.WriteLine("\t物品ID: {0} 名稱： {1}", item.ItemID, item.ItemName);
            }
        }
        private void AddItemCommandTask()
        {
            Console.Write("請輸入新物品ID(輸入cancel取消): ");
            int itemID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out itemID) || editingFactory.ContainsItem(itemID)))
                {
                    if (editingFactory.ContainsItem(itemID))
                    {
                        Console.Write("ID已存在 請輸入新物品ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新物品ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入物品名稱: ");
                    string itemName = Console.ReadLine();
                    editingFactory.AddItem(new Item(itemID, itemName));
                    ViewCommandTask();
                }
            }
        }
        private void RemoveItemCommandTask()
        {
            Console.Write("請輸入要刪除的物品ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int itemID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out itemID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的物品ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if (editingFactory.RemoveItem(itemID))
                {
                    Console.WriteLine("已刪除物品");
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

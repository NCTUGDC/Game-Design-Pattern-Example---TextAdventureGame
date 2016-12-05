using System;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.NPCs;

namespace TextAdventureGame.ConsoleEditor.NPC_FactoryEditorElements
{
    public class NPC_FactoryControlHandler : EditorControlHandler
    {
        private NPC_Factory editingFactory;

        public override string ControlInformation
        {
            get
            {
                return "NPC工廠編輯器： 輸入 help 了解操作方式";
            }
        }

        public NPC_FactoryControlHandler(NPC_Factory factory)
        {
            editingFactory = factory;
        }
        private void SaveFactory(string filePath)
        {
            NPC_Factory.SaveFactory(filePath, editingFactory);
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
                    case "add npc":
                        AddNPC_CommandTask();
                        break;
                    case "remove npc":
                        RemoveNPC_CommandTask();
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
            Console.WriteLine("\t輸入exit離開NPC工廠編輯器");
            Console.WriteLine("\t輸入save儲存NPC工廠(名稱先不用輸入)");
            Console.WriteLine("\t輸入view檢視NPC工廠資訊");
            Console.WriteLine("\t輸入add npc加入新NPC");
            Console.WriteLine("\t輸入remove npc移除NPC");
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
            Console.WriteLine("共有{0}位NPC", editingFactory.NPC_Count);
            foreach (var npc in editingFactory.NPCs)
            {
                Console.WriteLine("\tNPC ID: {0} 名稱： {1}, 預設對話： {2}" + ((npc is Seller) ? ("商店ID: " + (npc as Seller).StoreID.ToString()) : ""), npc.NPC_ID, npc.Name, npc.ConversationContent);
            }
        }
        private void AddNPC_CommandTask()
        {
            Console.Write("請輸入新NPC ID(輸入cancel取消): ");
            int npcID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out npcID) || editingFactory.ContainsNPC(npcID)))
                {
                    if (editingFactory.ContainsNPC(npcID))
                    {
                        Console.Write("ID已存在 請輸入新NPC ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新NPC ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入NPC名稱: ");
                    string npcName = Console.ReadLine();
                    Console.Write("請輸入預設對話: ");
                    string conversationContent = Console.ReadLine();
                    Console.Write("是否為商人?(y為確定) ");
                    if(Console.ReadLine() == "y")
                    {
                        Console.Write("請輸入商店 ID(輸入cancel取消): ");
                        int storeID = 0;
                        inputString = Console.ReadLine();
                        while (inputString != "cancel" && (!int.TryParse(inputString, out storeID)))
                        {
                            Console.Write("不合法的輸入 請輸入新NPC ID(整數)(輸入cancel取消): ");
                            inputString = Console.ReadLine();
                        }
                        if(inputString != "cancel")
                        {
                            editingFactory.AddNPC(new Seller(npcID, npcName, conversationContent, storeID));
                        }
                    }
                    else
                    {
                        editingFactory.AddNPC(new NPC(npcID, npcName, conversationContent));
                    }
                    ViewCommandTask();
                }
            }
        }
        private void RemoveNPC_CommandTask()
        {
            Console.Write("請輸入要刪除的NPCID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int npcID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out npcID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的NPC ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if (editingFactory.RemoveNPC(npcID))
                {
                    Console.WriteLine("已刪除NPC");
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

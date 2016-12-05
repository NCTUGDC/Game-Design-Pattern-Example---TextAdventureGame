using System;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.ConsoleEditor.WorldEditorElements
{
    public class SceneControlHandler : EditorControlHandler
    {
        private Scene editingScene;

        public override string ControlInformation
        {
            get
            {
                return "場景編輯器： 輸入 help 了解操作方式";
            }
        }

        public SceneControlHandler(Scene scene)
        {
            editingScene = scene;
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "back to world":
                        BackToWorldCommandTask(out rollbackLayerCount);
                        break;
                    case "add npc":
                        AddNPC_CommandTask();
                        break;
                    case "remove npc":
                        RemoveNPC_CommandTask();
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
            Console.WriteLine("\t輸入add npc加入新NPC");
            Console.WriteLine("\t輸入remove npc移除NPC");
            Console.WriteLine("\t輸入back to world返回世界層級");
        }
        private void BackToWorldCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        protected override void ViewCommandTask()
        {
            Console.WriteLine("場景ID: {0} 名稱: {1}, 共有{2}個NPC", editingScene.SceneID, editingScene.SceneName, editingScene.NPC_Count);
            Console.WriteLine("NPC:");
            foreach (var npcID in editingScene.NPC_IDs)
            {
                Console.WriteLine("\tNPCID: {0}", npcID);
            }
        }
        private void AddNPC_CommandTask()
        {
            Console.Write("請輸入新NPC ID(輸入cancel取消): ");
            int npcID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out npcID) || editingScene.ContainsNPC(npcID)))
                {
                    if (editingScene.ContainsNPC(npcID))
                    {
                        Console.Write("ID已存在 請輸入新NPC ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新NPC ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if(inputString != "cancel")
                {
                    editingScene.AddNPC_ID(npcID);
                }
                ViewCommandTask();
            }
        }
        private void RemoveNPC_CommandTask()
        {
            Console.Write("請輸入要刪除的NPC ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int npcID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out npcID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的NPC ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if (editingScene.RemoveNPC(npcID))
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

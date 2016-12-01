using System;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.ConsoleEditor.WorldEditorElements
{
    public class WorldControlHandler : EditorControlHandler
    {
        private World editingWorld;

        public override string ControlInformation
        {
            get
            {
                return "世界編輯器： 輸入 help 了解操作方式";
            }
        }

        public WorldControlHandler(World world)
        {
            editingWorld = world;
        }
        private void SaveWorld(string filePath)
        {
            World.SaveWorld(filePath, editingWorld);
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
                    case "add scene":
                        AddSceneCommandTask();
                        break;
                    case "load scene":
                        LoadSceneCommandTask();
                        break;
                    case "remove scene":
                        RemoveSceneCommandTask();
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
            Console.WriteLine("\t輸入exit離開世界編輯器");
            Console.WriteLine("\t輸入save儲存世界(名稱先不用輸入)");
            Console.WriteLine("\t輸入view檢視世界資訊");
            Console.WriteLine("\t輸入add scene加入新場景");
            Console.WriteLine("\t輸入load scene載入場景");
            Console.WriteLine("\t輸入remove scene移除場景");
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void SaveCommandTask()
        {
            Console.Write("請輸入要儲存的檔案路徑與名稱: ");
            SaveWorld(Console.ReadLine());
            Console.WriteLine("儲存成功!");
        }
        protected override void ViewCommandTask()
        {
            Console.WriteLine("世界名稱: {0}, 共有{1}個場景", editingWorld.WorldName, editingWorld.SceneCount);
            foreach (var scene in editingWorld.Scenes)
            {
                Console.WriteLine("\t場景ID: {0} 名稱： {1}", scene.SceneID, scene.SceneName);
            }
        }
        private void AddSceneCommandTask()
        {
            Console.Write("請輸入新場景ID(輸入cancel取消): ");
            int sceneID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sceneID) || editingWorld.ContainsScene(sceneID)))
                {
                    if (editingWorld.ContainsScene(sceneID))
                    {
                        Console.Write("ID已存在 請輸入新場景ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新場景ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入場景名稱: ");
                    string sceneName = Console.ReadLine();
                    editingWorld.AddScene(new Scene(sceneID, sceneName));
                    ViewCommandTask();
                }
            }
        }
        private void LoadSceneCommandTask()
        {
            Console.Write("請輸入要讀取的場景ID(輸入cancel取消): ");
            int sceneID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sceneID) || !editingWorld.ContainsScene(sceneID)))
                {
                    if (!editingWorld.ContainsScene(sceneID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的場景ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的場景ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new SceneControlHandler(editingWorld.FindScene(sceneID));
                }
            }
        }
        private void RemoveSceneCommandTask()
        {
            Console.Write("請輸入要刪除的場景ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int sceneID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out sceneID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的場景ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if(editingWorld.RemoveScene(sceneID))
                {
                    Console.WriteLine("已刪除場景");
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

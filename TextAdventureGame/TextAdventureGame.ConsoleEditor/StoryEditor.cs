using System;
using System.IO;
using TextAdventureGame.ConsoleEditor.StoryEditorElements;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.ConsoleEditor
{
    public class StoryEditor : EditorControlHandler
    {
        private Story editingStory;

        public override string ControlInformation
        {
            get
            {
                return "故事編輯器： 輸入 help 了解操作方式";
            }
        }

        private bool LoadStory(string filePath)
        {
            if(File.Exists(filePath))
            {
                editingStory = Story.LoadStory(filePath);
                editorControlHandler = new StoryContentControlHandler(editingStory);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateStory(int storyID, string storyName)
        {
            editingStory = new Story(storyID, storyName);
            editorControlHandler = new StoryContentControlHandler(editingStory);
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
                    case "load":
                        LoadCommandTask();
                        break;
                    case "create":
                        CreateCommandTask();
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
            Console.WriteLine("\t輸入exit離開編輯器");
            Console.WriteLine("\t輸入load讀取故事(名稱先不用輸入)");
            Console.WriteLine("\t輸入create建立新故事(名稱先不用輸入)");
        }
        protected override void ViewCommandTask()
        {
            HelpCommandTask();
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void LoadCommandTask()
        {
            Console.Write("請輸入要讀取的檔案路徑與名稱(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            while (inputString != "cancel" && !LoadStory(inputString))
            {
                Console.WriteLine("讀取失敗! 請輸入要讀取的檔案路徑與名稱(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                Console.WriteLine("讀取成功!");
            }
        }
        private void CreateCommandTask()
        {
            Console.Write("請輸入故事ID(輸入cancel取消): ");
            int storyID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out storyID))
                {
                    Console.Write("不合法的輸入 請輸入故事ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入故事名稱: ");
                    string storyName = Console.ReadLine();
                    CreateStory(storyID, storyName);
                    Console.WriteLine("建立成功!");
                }
            }
        }
        #endregion
    }
}

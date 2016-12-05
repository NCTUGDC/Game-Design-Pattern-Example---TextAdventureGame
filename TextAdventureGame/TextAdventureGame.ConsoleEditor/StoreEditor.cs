using System;
using System.IO;
using TextAdventureGame.ConsoleEditor.StoreFactoryEditorElements;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.ConsoleEditor
{
    public class StoreEditor :  EditorControlHandler
    {
        private StoreFactory editingFactory;

        public override string ControlInformation
        {
            get
            {
                return "商店編輯器： 輸入 help 了解操作方式";
            }
        }

        private bool LoadFactory(string filePath)
        {
            if (File.Exists(filePath))
            {
                editingFactory = StoreFactory.LoadFactory(filePath);
                editorControlHandler = new StoreFactoryControlHandler(editingFactory);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateFactory()
        {
            editingFactory = new StoreFactory();
            editorControlHandler = new StoreFactoryControlHandler(editingFactory);
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
            Console.WriteLine("\t輸入load讀取商店工廠(名稱先不用輸入)");
            Console.WriteLine("\t輸入create建立新商店工廠(名稱先不用輸入)");
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
            while (inputString != "cancel" && !LoadFactory(inputString))
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
            CreateFactory();
            Console.WriteLine("創造成功!");
        }
        #endregion
    }
}

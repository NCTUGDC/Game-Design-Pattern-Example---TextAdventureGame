using System;
using System.IO;
using TextAdventureGame.ConsoleEditor.PlotEditorElements;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.ConsoleEditor
{
    public class PlotEditor : EditorControlHandler
    {
        private Plot editingPlot;

        public override string ControlInformation
        {
            get
            {
                return "劇本編輯器： 輸入 help 了解操作方式";
            }
        }

        private bool LoadPlot(string filePath)
        {
            if(File.Exists(filePath))
            {
                editingPlot = Plot.LoadPlot(filePath);
                editorControlHandler = new PlotContentControlHandler(editingPlot);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreatePlot(int plotID, string plotName)
        {
            editingPlot = new Plot(plotID, plotName);
            editorControlHandler = new PlotContentControlHandler(editingPlot);
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
            Console.WriteLine("\t輸入load讀取劇本(名稱先不用輸入)");
            Console.WriteLine("\t輸入create建立新劇本(名稱先不用輸入)");
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void LoadCommandTask()
        {
            Console.Write("請輸入要讀取的檔案路徑與名稱(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            while (inputString != "cancel" && !LoadPlot(inputString))
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
            Console.Write("請輸入劇本ID(輸入cancel取消): ");
            int plotID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out plotID))
                {
                    Console.Write("不合法的輸入 請輸入劇本ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入劇本名稱: ");
                    string plotName = Console.ReadLine();
                    CreatePlot(plotID, plotName);
                    Console.WriteLine("建立成功!");
                }
            }
        }
        #endregion
    }
}

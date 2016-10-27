using System;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.PlotElements;

namespace TextAdventureGame.ConsoleEditor.PlotEditorElements
{
    public class PlotContentControlHandler : EditorControlHandler
    {
        private Plot editingPlot;

        public override string ControlInformation
        {
            get
            {
                return "劇本內容編輯器： 輸入 help 了解操作方式";
            }
        }

        public PlotContentControlHandler(Plot plot)
        {
            editingPlot = plot;
        }
        private void SavePlot(string filePath)
        {
            Plot.SavePlot(filePath, editingPlot);
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
                    case "view":
                        ViewCommandTask();
                        break;
                    case "add chapter":
                        AddChapterCommandTask();
                        break;
                    case "load chapter":
                        LoadChapterCommandTask();
                        break;
                    case "remove chapter":
                        RemoveChapterCommandTask();
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
            Console.WriteLine("\t輸入exit離開劇本編輯器");
            Console.WriteLine("\t輸入save儲存劇本(名稱先不用輸入)");
            Console.WriteLine("\t輸入view檢視劇本資訊");
            Console.WriteLine("\t輸入add chapter加入新篇章");
            Console.WriteLine("\t輸入load chapter載入篇章");
            Console.WriteLine("\t輸入remove chapter移除篇章");
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void SaveCommandTask()
        {
            Console.Write("請輸入要儲存的檔案路徑與名稱: ");
            SavePlot(Console.ReadLine());
            Console.WriteLine("儲存成功!");
        }
        private void ViewCommandTask()
        {
            Console.WriteLine("劇本ID: {0} 名稱: {1}, 共有{2}章", editingPlot.PlotID, editingPlot.PlotName, editingPlot.ChapterCount);
            foreach (var chapter in editingPlot.Chapters)
            {
                Console.WriteLine("\t篇章ID: {0} 名稱： {1}, 節數： {2}", chapter.ChapterID, chapter.ChapterName, chapter.SectionCount);
            }
        }
        private void AddChapterCommandTask()
        {
            Console.Write("請輸入新篇章ID(輸入cancel取消): ");
            int chapterID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out chapterID) || editingPlot.ContainsChapter(chapterID)))
                {
                    if (editingPlot.ContainsChapter(chapterID))
                    {
                        Console.Write("ID已存在 請輸入新篇章ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新篇章ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入篇章名稱: ");
                    string chapterName = Console.ReadLine();
                    editingPlot.AddChapter(new Chapter(chapterID, chapterName));
                    ViewCommandTask();
                }
            }
        }
        private void LoadChapterCommandTask()
        {
            Console.Write("請輸入要讀取的篇章ID(輸入cancel取消): ");
            int chapterID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out chapterID) || !editingPlot.ContainsChapter(chapterID)))
                {
                    if (!editingPlot.ContainsChapter(chapterID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的篇章ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的篇章ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new ChapterContentControlHandler(editingPlot.FindChapter(chapterID));
                }
            }
        }
        private void RemoveChapterCommandTask()
        {
            Console.Write("請輸入要刪除的篇章ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int chapterID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out chapterID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的篇章ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = editingPlot.RemoveChapter(chapterID);
                Console.WriteLine("共刪除{0}個篇章", removedCount);
            }
        }
        #endregion
    }
}

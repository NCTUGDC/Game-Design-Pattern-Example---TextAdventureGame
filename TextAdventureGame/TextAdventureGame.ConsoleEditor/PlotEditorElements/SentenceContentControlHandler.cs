using System;
using TextAdventureGame.Library.General.PlotElements;

namespace TextAdventureGame.ConsoleEditor.PlotEditorElements
{
    public class SentenceContentControlHandler : EditorControlHandler
    {
        private Sentence editingSentence;

        public override string ControlInformation
        {
            get
            {
                return "文句內容編輯器： 輸入 help 了解操作方式";
            }
        }

        public SentenceContentControlHandler(Sentence sentence)
        {
            editingSentence = sentence;
        }
        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "back to plot":
                        BackToPlotCommandTask(out rollbackLayerCount);
                        break;
                    case "back to chapter":
                        BackToChapterCommandTask(out rollbackLayerCount);
                        break;
                    case "back to section":
                        BackToSectionCommandTask(out rollbackLayerCount);
                        break;
                    case "back to paragraph":
                        BackToParagraphCommandTask(out rollbackLayerCount);
                        break;
                    case "view":
                        ViewCommandTask();
                        break;
                    case "add line":
                        AddLineCommandTask();
                        break;
                    case "remove line":
                        RemoveLineCommandTask();
                        break;
                    case "insert line":
                        InsertLineCommandTask();
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
            Console.WriteLine("\t輸入back to plot返回劇本層級");
            Console.WriteLine("\t輸入back to chapter返回篇章層級");
            Console.WriteLine("\t輸入back to section返回章節層級");
            Console.WriteLine("\t輸入back to paragraph返回段落層級");
            Console.WriteLine("\t輸入view檢視文句資訊");
            Console.WriteLine("\t輸入add line加入新行");
            Console.WriteLine("\t輸入remove line移除行");
            Console.WriteLine("\t輸入insert line插入行");
        }
        private void BackToPlotCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 4;
        }
        private void BackToChapterCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 3;
        }
        private void BackToSectionCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 2;
        }
        private void BackToParagraphCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void ViewCommandTask()
        {
            Console.WriteLine("文句ID: {0} 角色：{1} , 共有{1}行", editingSentence.SentenceID, editingSentence.SpeakerName, editingSentence.LineCount);
            foreach (var line in editingSentence.Lines)
            {
                Console.WriteLine("\t{0}", line);
            }
        }
        private void AddLineCommandTask()
        {
            Console.Write("請輸入內容(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                editingSentence.AddLine(inputString);
                ViewCommandTask();
            }
        }
        private void RemoveLineCommandTask()
        {
            Console.Write("請輸入要刪除第幾行(從零開始，輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int sectionID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out sectionID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除第幾行(從零開始，輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                if(editingSentence.RemoveLine(sectionID))
                {
                    Console.WriteLine("已刪除");
                }
                else
                {
                    Console.WriteLine("刪除失敗，請確認刪除的行數是否正確");
                }
            }
        }
        private void InsertLineCommandTask()
        {
            Console.Write("請輸入要插入的行數位置(輸入cancel取消): ");
            int insertIndex = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out insertIndex))
                {
                    Console.Write("不合法的輸入 請輸入要插入的行數位置(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入內容(輸入cancel取消): ");
                    string contentString = Console.ReadLine();
                    if (contentString != "cancel")
                    {
                        if(!editingSentence.InsertLine(insertIndex, contentString))
                        {
                            Console.WriteLine("插入失敗，請確認行數是否正確");
                        }
                        ViewCommandTask();
                    }
                }
            }
        }
        #endregion
    }
}

using System;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.ConsoleEditor.StoryEditorElements
{
    public class SectionContentControlHandler : PlotTriggerContentControlHandler
    {
        private Section editingSection;

        public override string ControlInformation
        {
            get
            {
                return "章節內容編輯器： 輸入 help 了解操作方式";
            }
        }

        public SectionContentControlHandler(Section section) : base(section)
        {
            editingSection = section;
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "back to story":
                        BackToStoryCommandTask(out rollbackLayerCount);
                        break;
                    case "back to chapter":
                        BackToChapterCommandTask(out rollbackLayerCount);
                        break;
                    case "add paragraph":
                        AddParagraphCommandTask();
                        break;
                    case "load paragraph":
                        LoadParagraphCommandTask();
                        break;
                    case "remove paragraph":
                        RemoveParagraphCommandTask();
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
            Console.WriteLine("\t輸入add paragraph加入新段落");
            Console.WriteLine("\t輸入load paragraph載入段落");
            Console.WriteLine("\t輸入remove paragraph移除段落");
            Console.WriteLine("\t輸入back to story返回故事層級");
            Console.WriteLine("\t輸入back to chapter返回篇章層級");
        }
        private void BackToStoryCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 2;
        }
        private void BackToChapterCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        protected override void ViewCommandTask()
        {
            Console.WriteLine("章節ID: {0} 名稱: {1}, 共有{2}段落", editingSection.SectionID, editingSection.SectionName, editingSection.ParagraphCount);
            base.ViewCommandTask();
            Console.WriteLine("目錄:");
            foreach (var paragraph in editingSection.Paragraphs)
            {
                Console.WriteLine("\t段落ID: {0} , 文句數： {1}", paragraph.ParagraphID, paragraph.SentenceCount);
            }
        }
        private void AddParagraphCommandTask()
        {
            Console.Write("請輸入新段落ID(輸入cancel取消): ");
            int paragraphID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out paragraphID) || editingSection.ContainsParagraph(paragraphID)))
                {
                    if (editingSection.ContainsParagraph(paragraphID))
                    {
                        Console.Write("ID已存在 請輸入新段落ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新段落ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editingSection.AddParagraph(new Paragraph(paragraphID));
                    ViewCommandTask();
                }
            }
        }
        private void LoadParagraphCommandTask()
        {
            Console.Write("請輸入要讀取的段落ID(輸入cancel取消): ");
            int paragraphID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out paragraphID) || !editingSection.ContainsParagraph(paragraphID)))
                {
                    if (!editingSection.ContainsParagraph(paragraphID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的段落ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的段落ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new ParagraphContentControlHandler(editingSection.FindParagraph(paragraphID));
                }
            }
        }
        private void RemoveParagraphCommandTask()
        {
            Console.Write("請輸入要刪除的段落ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int sectionID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out sectionID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的段落ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = editingSection.RemoveParagraph(sectionID);
                Console.WriteLine("共刪除{0}個段落", removedCount);
            }
        }
        #endregion
    }
}

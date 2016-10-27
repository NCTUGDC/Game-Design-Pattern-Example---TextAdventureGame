using System;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.ConsoleEditor.StoryEditorElements
{
    public class ChapterContentControlHandler : EditorControlHandler
    {
        private Chapter editingChapter;

        public override string ControlInformation
        {
            get
            {
                return "篇章內容編輯器： 輸入 help 了解操作方式";
            }
        }

        public ChapterContentControlHandler(Chapter chapter)
        {
            editingChapter = chapter;
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
                    case "view":
                        ViewCommandTask();
                        break;
                    case "add section":
                        AddSectionCommandTask();
                        break;
                    case "load section":
                        LoadSectionCommandTask();
                        break;
                    case "remove section":
                        RemoveSectionCommandTask();
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
            Console.WriteLine("\t輸入back to story返回故事層級");
            Console.WriteLine("\t輸入view檢視篇章資訊");
            Console.WriteLine("\t輸入add section加入新章節");
            Console.WriteLine("\t輸入load section載入章節");
            Console.WriteLine("\t輸入remove section移除章節");
        }
        private void BackToStoryCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void ViewCommandTask()
        {
            Console.WriteLine("篇章ID: {0} 名稱: {1}, 共有{2}節", editingChapter.ChapterID, editingChapter.ChapterName, editingChapter.SectionCount);
            foreach (var section in editingChapter.Sections)
            {
                Console.WriteLine("\t章節ID: {0} 名稱： {1}, 段落數： {2}", section.SectionID, section.SectionName, section.ParagraphCount);
            }
        }
        private void AddSectionCommandTask()
        {
            Console.Write("請輸入新章節ID(輸入cancel取消): ");
            int sectionID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sectionID) || editingChapter.ContainsSection(sectionID)))
                {
                    if (editingChapter.ContainsSection(sectionID))
                    {
                        Console.Write("ID已存在 請輸入新章節ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新章節ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入章節名稱: ");
                    string sectionName = Console.ReadLine();
                    editingChapter.AddSection(new Section(sectionID, sectionName));
                    ViewCommandTask();
                }
            }
        }
        private void LoadSectionCommandTask()
        {
            Console.Write("請輸入要讀取的章節ID(輸入cancel取消): ");
            int sectionID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sectionID) || !editingChapter.ContainsSection(sectionID)))
                {
                    if (!editingChapter.ContainsSection(sectionID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的章節ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的章節ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new SectionContentControlHandler(editingChapter.FindSection(sectionID));
                }
            }
        }
        private void RemoveSectionCommandTask()
        {
            Console.Write("請輸入要刪除的章節ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int sectionID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out sectionID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的章節ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = editingChapter.RemoveSection(sectionID);
                Console.WriteLine("共刪除{0}個章節", removedCount);
            }
        }
        #endregion
    }
}

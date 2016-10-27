using System;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.ConsoleEditor.StoryEditorElements
{
    public class ParagraphContentControlHandler : EditorControlHandler
    {
        private Paragraph editingParagraph;

        public override string ControlInformation
        {
            get
            {
                return "段落內容編輯器： 輸入 help 了解操作方式";
            }
        }

        public ParagraphContentControlHandler(Paragraph paragraph)
        {
            editingParagraph = paragraph;
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
                    case "back to section":
                        BackToSectionCommandTask(out rollbackLayerCount);
                        break;
                    case "view":
                        ViewCommandTask();
                        break;
                    case "add sentence":
                        AddSentenceCommandTask();
                        break;
                    case "load sentence":
                        LoadSentenceCommandTask();
                        break;
                    case "remove sentence":
                        RemoveSentenceCommandTask();
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
            Console.WriteLine("\t輸入back to chapter返回篇章層級");
            Console.WriteLine("\t輸入back to section返回章節層級");
            Console.WriteLine("\t輸入view檢視段落資訊");
            Console.WriteLine("\t輸入add sentence加入新文句");
            Console.WriteLine("\t輸入load sentence載入文句");
            Console.WriteLine("\t輸入remove sentence移除文句");
        }
        private void BackToStoryCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 3;
        }
        private void BackToChapterCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 2;
        }
        private void BackToSectionCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void ViewCommandTask()
        {
            Console.WriteLine("段落ID: {0} , 共有{1}條文句", editingParagraph.ParagraphID, editingParagraph.SentenceCount);
            foreach (var sentence in editingParagraph.Sentences)
            {
                Console.WriteLine("\t文句ID: {0} 角色：{1} , 行數： {2}", sentence.SentenceID, sentence.SpeakerName, sentence.LineCount);
            }
        }
        private void AddSentenceCommandTask()
        {
            Console.Write("請輸入新文句ID(輸入cancel取消): ");
            int sentenceID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sentenceID) || editingParagraph.ContainsSentence(sentenceID)))
                {
                    if (editingParagraph.ContainsSentence(sentenceID))
                    {
                        Console.Write("ID已存在 請輸入新文句ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入新文句ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.Write("請輸入角色名稱: ");
                    string speakerName = Console.ReadLine();
                    editingParagraph.AddSentence(new Sentence(sentenceID, speakerName));
                    ViewCommandTask();
                }
            }
        }
        private void LoadSentenceCommandTask()
        {
            Console.Write("請輸入要讀取的文句ID(輸入cancel取消): ");
            int sentenceID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && (!int.TryParse(inputString, out sentenceID) || !editingParagraph.ContainsSentence(sentenceID)))
                {
                    if (!editingParagraph.ContainsSentence(sentenceID))
                    {
                        Console.Write("ID不存在 請輸入要讀取的文句ID(整數)(輸入cancel取消): ");
                    }
                    else
                    {
                        Console.Write("不合法的輸入 請輸入要讀取的文句ID(整數)(輸入cancel取消): ");
                    }
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    editorControlHandler = new SentenceContentControlHandler(editingParagraph.FindSentence(sentenceID));
                }
            }
        }
        private void RemoveSentenceCommandTask()
        {
            Console.Write("請輸入要刪除的文句ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int sectionID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out sectionID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的文句ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = editingParagraph.RemoveSentence(sectionID);
                Console.WriteLine("共刪除{0}條文句", removedCount);
            }
        }
        #endregion
    }
}

using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.StoryElements
{
    public class Section
    {
        [MessagePackMember(id: 0, Name = "SectionID")]
        public int SectionID { get; private set; }
        [MessagePackMember(id: 1, Name = "SectionName")]
        public string SectionName { get; private set; }
        [MessagePackMember(id: 2, Name = "paragraphs")]
        private List<Paragraph> paragraphs;
        [MessagePackMember(id: 3, Name = "currentParagraphIndex")]
        private int currentParagraphIndex;

        public bool IsEnd { get { return currentParagraphIndex == paragraphs.Count - 1; } }
        public Paragraph CurrentParagraph { get { return (currentParagraphIndex >= 0) ? paragraphs[currentParagraphIndex] : null; } }
        public int ParagraphCount { get { return paragraphs.Count; } }
        public IEnumerable<Paragraph> Paragraphs { get { return paragraphs; } }

        [MessagePackDeserializationConstructor]
        public Section() { }
        public Section(int sectionID, string sectionName)
        {
            SectionID = sectionID;
            SectionName = sectionName;
            paragraphs = new List<Paragraph>();
            currentParagraphIndex = -1;
        }

        public bool ContainsParagraph(int paragraphID)
        {
            return paragraphs.Any(x => x.ParagraphID == paragraphID);
        }
        public Paragraph FindParagraph(int paragraphID)
        {
            return paragraphs.Find(x => x.ParagraphID == paragraphID);
        }
        public bool AddParagraph(Paragraph paragraph)
        {
            if (paragraph != null && !ContainsParagraph(paragraph.ParagraphID))
            {
                paragraphs.Add(paragraph);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int RemoveParagraph(int paragraphID)
        {
            return paragraphs.RemoveAll(x => x.ParagraphID == paragraphID);
        }

        public bool ToNextParagraph()
        {
            if (IsEnd)
            {
                return false;
            }
            else
            {
                currentParagraphIndex++;
                return true;
            }
        }
        public bool JumpToParagraph(int paragraphID)
        {
            if (paragraphs.Any(x => x.ParagraphID == paragraphID))
            {
                currentParagraphIndex = paragraphs.FindIndex(x => x.ParagraphID == paragraphID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JumpToStart()
        {
            currentParagraphIndex = -1;
        }
    }
}

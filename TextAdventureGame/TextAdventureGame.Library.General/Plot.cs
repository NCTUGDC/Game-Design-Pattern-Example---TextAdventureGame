using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextAdventureGame.Library.General.PlotElements;

namespace TextAdventureGame.Library.General
{
    public class Plot
    {
        public static Plot LoadPlot(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<Plot>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SavePlot(string fileName, Plot plot)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(plot));
        }

        [MessagePackMember(id: 0, Name = "PlotID")]
        public int PlotID { get; set; }
        [MessagePackMember(id: 1, Name = "PlotName")]
        public string PlotName { get; set; }
        [MessagePackMember(id: 2, Name = "chapters")]
        private List<Chapter> chapters;
        [MessagePackMember(id: 3, Name = "currentChapterIndex")]
        private int currentChapterIndex;

        public bool IsEnd { get { return currentChapterIndex == chapters.Count - 1; } }
        public Chapter CurrentChapter { get { return (currentChapterIndex >= 0) ? chapters[currentChapterIndex] : null; } }
        public int ChapterCount { get { return chapters.Count; } }
        public IEnumerable<Chapter> Chapters { get { return chapters; } }

        [MessagePackDeserializationConstructor]
        public Plot(){}

        public Plot(int plotID, string plotName)
        {
            PlotID = plotID;
            PlotName = plotName;
            chapters = new List<Chapter>();
            currentChapterIndex = -1;
        }
        public bool ContainsChapter(int chapterID)
        {
            return chapters.Any(x => x.ChapterID == chapterID);
        }
        public Chapter FindChapter(int chapterID)
        {
            return chapters.Find(x => x.ChapterID == chapterID);
        }
        public bool AddChapter(Chapter chapter)
        {
            if (chapter != null && !ContainsChapter(chapter.ChapterID))
            {
                chapters.Add(chapter);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int RemoveChapter(int chapterID)
        {
            return chapters.RemoveAll(x => x.ChapterID == chapterID);
        }

        public bool ToNextChapter()
        {
            if (IsEnd)
            {
                return false;
            }
            else
            {
                currentChapterIndex++;
                return true;
            }
        }
        public bool JumpToChapter(int chapterID)
        {
            if (chapters.Any(x => x.ChapterID == chapterID))
            {
                currentChapterIndex = chapters.FindIndex(x => x.ChapterID == chapterID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JumpToStart()
        {
            currentChapterIndex = -1;
        }
    }
}

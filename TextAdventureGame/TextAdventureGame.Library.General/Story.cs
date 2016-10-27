using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.Library.General
{
    public class Story
    {
        public static Story LoadStory(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<Story>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveStory(string fileName, Story story)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(story));
        }

        [MessagePackMember(id: 0, Name = "StoryID")]
        public int StoryID { get; set; }
        [MessagePackMember(id: 1, Name = "StoryName")]
        public string StoryName { get; set; }
        [MessagePackMember(id: 2, Name = "chapters")]
        private List<Chapter> chapters;
        [MessagePackMember(id: 3, Name = "currentChapterIndex")]
        private int currentChapterIndex;

        public bool IsEnd { get { return currentChapterIndex == chapters.Count - 1; } }
        public Chapter CurrentChapter { get { return (currentChapterIndex >= 0) ? chapters[currentChapterIndex] : null; } }
        public int ChapterCount { get { return chapters.Count; } }
        public IEnumerable<Chapter> Chapters { get { return chapters; } }

        [MessagePackDeserializationConstructor]
        public Story(){}

        public Story(int storyID, string storyName)
        {
            StoryID = storyID;
            StoryName = storyName;
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

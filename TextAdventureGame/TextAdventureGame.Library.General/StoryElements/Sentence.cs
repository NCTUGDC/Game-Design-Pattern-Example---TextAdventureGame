using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements
{
    public class Sentence
    {
        [MessagePackMember(id: 0, Name = "SentenceID")]
        public int SentenceID { get; private set; }
        [MessagePackMember(id: 1, Name = "SpeakerName")]
        public string SpeakerName { get; set; }
        [MessagePackMember(id: 2, Name = "lines")]
        private List<string> lines;
        [MessagePackMember(id: 3, Name = "currentLineIndex")]
        private int currentLineIndex;

        public bool IsEnd { get { return currentLineIndex == lines.Count - 1; } }
        public string CurrentLine { get { return (currentLineIndex >= 0) ? lines[currentLineIndex] : ""; } }
        public int LineCount { get { return lines.Count; } }
        public IEnumerable<string> Lines { get { return lines; } }

        [MessagePackDeserializationConstructor]
        public Sentence() { }
        public Sentence(int sentenctID, string speakerName)
        {
            SentenceID = sentenctID;
            SpeakerName = speakerName;
            lines = new List<string>();
            currentLineIndex = -1;
        }

        public bool AddLine(string line)
        {
            if (line != null)
            {
                lines.Add(line);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertLine(int index, string line)
        {
            if (index >= 0 && index < lines.Count)
            {
                lines.Insert(index, line);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveLine(int lineIndex)
        {
            if(lineIndex >= 0 && lineIndex < lines.Count)
            {
                lines.RemoveAt(lineIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ToNextLine()
        {
            if(IsEnd)
            {
                return false;
            }
            else
            {
                currentLineIndex++;
                return true;
            }
        }
        public bool JumpToLine(int lineNumber)
        {
            if(lineNumber >= 0 && lineNumber < lines.Count)
            {
                currentLineIndex = lineNumber;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JumpToStart()
        {
            currentLineIndex = -1;
        }
    }
}

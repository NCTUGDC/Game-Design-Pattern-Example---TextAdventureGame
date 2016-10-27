using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.PlotElements
{
    public class Paragraph
    {
        [MessagePackMember(id: 0, Name = "ParagraphID")]
        public int ParagraphID { get; private set; }
        [MessagePackMember(id: 1, Name = "sentences")]
        private List<Sentence> sentences;
        [MessagePackMember(id: 2, Name = "currentSentenceIndex")]
        private int currentSentenceIndex;

        public bool IsEnd { get { return currentSentenceIndex == sentences.Count - 1; } }
        public Sentence CurrentSentence { get { return (currentSentenceIndex >= 0) ? sentences[currentSentenceIndex] : null; } }
        public int SentenceCount { get { return sentences.Count; } }
        public IEnumerable<Sentence> Sentences { get { return sentences; } }

        [MessagePackDeserializationConstructor]
        public Paragraph() { }
        public Paragraph(int paragraphID)
        {
            ParagraphID = paragraphID;
            sentences = new List<Sentence>();
            currentSentenceIndex = -1;
        }

        public bool ContainsSentence(int sentenceID)
        {
            return sentences.Any(x => x.SentenceID == sentenceID);
        }
        public Sentence FindSentence(int sentenceID)
        {
            return sentences.Find(x => x.SentenceID == sentenceID);
        }
        public bool AddSentence(Sentence sentence)
        {
            if (sentence != null && !ContainsSentence(sentence.SentenceID))
            {
                sentences.Add(sentence);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int RemoveSentence(int sentenceID)
        {
            return sentences.RemoveAll(x => x.SentenceID == sentenceID);
        }

        public bool ToNextSentence()
        {
            if (IsEnd)
            {
                return false;
            }
            else
            {
                currentSentenceIndex++;
                return true;
            }
        }
        public bool JumpToSentence(int sentenceID)
        {
            if(sentences.Any(x => x.SentenceID == sentenceID))
            {
                currentSentenceIndex = sentences.FindIndex(x => x.SentenceID == sentenceID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JumpToStart()
        {
            currentSentenceIndex = -1;
        }
    }
}

using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.StoryElements;
using UnityEngine;

namespace TextAdventureGame.Unity.Scripts.StoryScripts
{
    public class SentenceManager : MonoBehaviour
    {
        private static SentenceManager instance;
        public static SentenceManager Instance { get { return instance; } }

        [SerializeField]
        private SentenceDialog sentenceDialogPrefab;
        private Story story;
        private Canvas canvas;

        void Start()
        {
            instance = this;
            story = StoryManager.Instance.Story;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            story.JumpToChapter(1);
            story.CurrentChapter.JumpToSection(2);
            story.CurrentChapter.CurrentSection.JumpToParagraph(1);
            story.CurrentChapter.CurrentSection.CurrentParagraph.JumpToSentence(1);
            ToNext();
        }

        public void ToNext()
        {
            if(story.CurrentChapter == null)
            {
                ToNextChapter(story);
            }
            else if(story.CurrentChapter.CurrentSection == null)
            {
                ToNextSection(story.CurrentChapter);
            }
            else if(story.CurrentChapter.CurrentSection.CurrentParagraph == null)
            {
                ToNextParagraph(story.CurrentChapter.CurrentSection);
            }
            else if(story.CurrentChapter.CurrentSection.CurrentParagraph.CurrentSentence == null)
            {
                ToNextSentence(story.CurrentChapter.CurrentSection.CurrentParagraph);
            }
            else if(!story.CurrentChapter.CurrentSection.CurrentParagraph.CurrentSentence.IsEnd)
            {
                ShowSentence(story.CurrentChapter.CurrentSection.CurrentParagraph.CurrentSentence);
            }
            else if(!story.CurrentChapter.CurrentSection.CurrentParagraph.IsEnd)
            {
                ToNextSentence(story.CurrentChapter.CurrentSection.CurrentParagraph);
            }
            else if (!story.CurrentChapter.CurrentSection.IsEnd)
            {
                ToNextParagraph(story.CurrentChapter.CurrentSection);
            }
            else if (!story.CurrentChapter.IsEnd)
            {
                ToNextSection(story.CurrentChapter);
            }
            else if (!story.IsEnd)
            {
                ToNextChapter(story);
            }
        }
        private void ToNextChapter(Story story)
        {
            if (story.ToNextChapter())
            {
                ToNextSection(story.CurrentChapter);
            }
        }
        private void ToNextSection(Chapter chapter)
        {
            if (chapter.ToNextSection())
            {
                ToNextParagraph(chapter.CurrentSection);
            }
        }
        private void ToNextParagraph(Section section)
        {
            if (section.ToNextParagraph())
            {
                ToNextSentence(section.CurrentParagraph);
            }
        }
        private void ToNextSentence(Paragraph paragraph)
        {
            if (paragraph.ToNextSentence())
            {
                paragraph.CurrentSentence.JumpToLine(0);
                if (paragraph.CurrentSentence.IsEnd)
                {
                    paragraph.CurrentSentence.ExecuteEvents();
                }
                ShowSentence(paragraph.CurrentSentence);
            }
        }
        void ShowSentence(Sentence sentence)
        {
            if(sentence != null)
            {
                SentenceDialog dialog = Instantiate(sentenceDialogPrefab);
                dialog.transform.SetParent(canvas.transform);
                dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -225);

                dialog.Sentence = sentence;
            }
        }
    }
}

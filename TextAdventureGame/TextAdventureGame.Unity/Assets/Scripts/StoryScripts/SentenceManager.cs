using UnityEngine;
using UnityEngine.UI;
using System;
using TextAdventureGame.Unity.Library;
using TextAdventureGame.Library.General;

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

        void Awake()
        {
            instance = this;
            story = GameManager.Game.MainStory;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        void Start()
        {
            story.ToNextChapter();
            story.CurrentChapter.ToNextSection();
            story.CurrentChapter.CurrentSection.ToNextParagraph();
            story.CurrentChapter.CurrentSection.CurrentParagraph.ToNextSentence();
            ShowSentence();
        }

        public void ToNextSentence()
        {
            if(story.CurrentChapter.CurrentSection.CurrentParagraph.ToNextSentence())
            {
                ShowSentence();
            }
        }
        void ShowSentence()
        {
            SentenceDialog dialog = Instantiate(sentenceDialogPrefab);
            dialog.transform.SetParent(canvas.transform);
            dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -225);

            dialog.Sentence = story.CurrentChapter.CurrentSection.CurrentParagraph.CurrentSentence;
        }
    }
}

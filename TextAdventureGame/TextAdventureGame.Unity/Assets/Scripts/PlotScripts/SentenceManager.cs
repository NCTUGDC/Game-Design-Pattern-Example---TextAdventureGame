using UnityEngine;
using UnityEngine.UI;
using System;
using TextAdventureGame.Unity.Library;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.Unity.Scripts.PlotScripts
{
    public class SentenceManager : MonoBehaviour
    {
        private static SentenceManager instance;
        public static SentenceManager Instance { get { return instance; } }

        [SerializeField]
        private SentenceDialog sentenceDialogPrefab;
        private Plot plot;
        private Canvas canvas;

        void Awake()
        {
            instance = this;
            plot = GameManager.Game.MainPlot;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        void Start()
        {
            plot.ToNextChapter();
            plot.CurrentChapter.ToNextSection();
            plot.CurrentChapter.CurrentSection.ToNextParagraph();
            plot.CurrentChapter.CurrentSection.CurrentParagraph.ToNextSentence();
            ShowSentence();
        }

        public void ToNextSentence()
        {
            plot.CurrentChapter.CurrentSection.CurrentParagraph.ToNextSentence();
            ShowSentence();
        }
        void ShowSentence()
        {
            SentenceDialog dialog = Instantiate(sentenceDialogPrefab);
            dialog.transform.SetParent(canvas.transform);
            dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -225);

            dialog.Sentence = plot.CurrentChapter.CurrentSection.CurrentParagraph.CurrentSentence;
        }
    }
}

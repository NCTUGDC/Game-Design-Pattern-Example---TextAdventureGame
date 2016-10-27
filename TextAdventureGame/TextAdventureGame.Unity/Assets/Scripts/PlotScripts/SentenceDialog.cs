using TextAdventureGame.Library.General.PlotElements;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.PlotScripts
{
    public class SentenceDialog : MonoBehaviour
    {
        private Button dialogButton;
        private Text speakerNameLabel;
        private Text dialogTextArea;

        private Sentence sentence;
        public Sentence Sentence
        {
            private get { return sentence; }
            set
            {
                if(value != null)
                {
                    sentence = value;
                    
                    speakerNameLabel.text = Sentence.SpeakerName;
                    Sentence.ToNextLine();
                    dialogTextArea.text = Sentence.CurrentLine;
                }
            }
        }

        void Awake()
        {
            dialogButton = GetComponent<Button>();
            speakerNameLabel = transform.Find("SpeakerNameLabel").GetComponent<Text>();
            dialogTextArea = transform.Find("DialogTextArea").GetComponent<Text>();

            dialogButton.onClick.AddListener(NextLine);
        }

        void NextLine()
        {
            if(Sentence != null)
            {
                if (Sentence.ToNextLine())
                {
                    dialogTextArea.text = Sentence.CurrentLine;
                }
                else
                {
                    SentenceManager.Instance.ToNextSentence();
                    Destroy(gameObject);
                }
            }
        }
    }
}
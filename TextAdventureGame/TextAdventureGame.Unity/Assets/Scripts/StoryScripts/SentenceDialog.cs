using TextAdventureGame.Library.General.StoryElements;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.StoryScripts
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
                sentence = value;
                speakerNameLabel.text = sentence.SpeakerName;
                dialogTextArea.text = sentence.CurrentLine;
                if(sentence.CurrentLine == "")
                {
                    NextLine();
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
                    SentenceManager.Instance.ToNext();
                    Destroy(gameObject);
                }
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class TalkDialog : MonoBehaviour
    {
        private Text npcNameText;
        private Text dialogTextArea;

        private void Awake()
        {
            npcNameText = transform.Find("NPC_NameText").GetComponent<Text>();
            dialogTextArea = transform.Find("DialogTextArea").GetComponent<Text>();
            GetComponent<Button>().onClick.AddListener(() => Destroy(gameObject));
        }
        public void Initial(NPC npc)
        {
            npcNameText.text = npc.Name;
            dialogTextArea.text = npc.ConversationContent;
        }
    }
}

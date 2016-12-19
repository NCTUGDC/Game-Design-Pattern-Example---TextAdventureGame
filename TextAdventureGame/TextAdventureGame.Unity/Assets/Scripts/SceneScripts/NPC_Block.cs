using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.NPCs;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class NPC_Block : MonoBehaviour
    {
        private Text npcNameText;
        private Button talkButton;
        private Button storeButton;

        private void Awake()
        {
            npcNameText = transform.Find("NPC_NameText").GetComponent<Text>();
            talkButton = transform.Find("TalkButton").GetComponent<Button>();
            storeButton = transform.Find("StoreButton").GetComponent<Button>();
        }

        public void Initial(NPC npc)
        {
            npcNameText.text = npc.Name;
            talkButton.onClick.AddListener(() => SceneManager.Instance.TalkWithNPC(npc));
            if(npc is Seller)
            {
                storeButton.gameObject.SetActive(true);
                storeButton.onClick.AddListener(() => SceneManager.Instance.OpenStore((npc as Seller).StoreID));
            }
        }
    }
}

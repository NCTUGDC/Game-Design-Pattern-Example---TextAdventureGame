using TextAdventureGame.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.BattleScripts
{
    public class PlayerBlock : MonoBehaviour
    {
        private Text playerNameText;
        private Scrollbar hpScrollbar;
        private Scrollbar spScrollbar;
        private RectTransform statusContent;

        public StatusBlock statusBlockPrefab;

        void Awake()
        {
            playerNameText = transform.Find("PlayerNameText").GetComponent<Text>();
            hpScrollbar = transform.Find("HP_Scrollbar").GetComponent<Scrollbar>();
            spScrollbar = transform.Find("SP_Scrollbar").GetComponent<Scrollbar>();
            statusContent = transform.Find("StatusScrollView").Find("Viewport").Find("Content").GetComponent<RectTransform>();
        }

        public void Render(BattleSystem battleSystem)
        {
            playerNameText.text = battleSystem.Player.Name;
            hpScrollbar.size = battleSystem.Player.AbilityFactors.HP / (float)battleSystem.Player.AbilityFactors.MaxHP;
            spScrollbar.size = battleSystem.Player.AbilityFactors.SP / (float)battleSystem.Player.AbilityFactors.MaxSP;

            foreach (Transform child in statusContent)
            {
                Destroy(child.gameObject);
            }
            statusContent.sizeDelta = new Vector2(statusContent.sizeDelta.x, 60 * battleSystem.PlayerSkillEffectStatuses.Count);
            foreach (var status in battleSystem.PlayerSkillEffectStatuses)
            {
                StatusBlock statusBlock = Instantiate(statusBlockPrefab);
                statusBlock.transform.SetParent(statusContent);
                statusBlock.Initial(status);
            }
        }
    }
}

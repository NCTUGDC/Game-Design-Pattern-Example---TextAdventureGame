using TextAdventureGame.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.BattleScripts
{
    public class EnemyBlock : MonoBehaviour
    {
        private Text enemyNameText;
        private Scrollbar hpScrollbar;
        private Scrollbar spScrollbar;
        private RectTransform statusContent;

        public StatusBlock statusBlockPrefab;

        void Awake()
        {
            enemyNameText = transform.Find("EnemyNameText").GetComponent<Text>();
            hpScrollbar = transform.Find("HP_Scrollbar").GetComponent<Scrollbar>();
            spScrollbar = transform.Find("SP_Scrollbar").GetComponent<Scrollbar>();
            statusContent = transform.Find("StatusScrollView").Find("Viewport").Find("Content").GetComponent<RectTransform>();
        }

        public void Render(BattleSystem battleSystem, int enemyIndex)
        {
            enemyNameText.text = battleSystem.Monsters[enemyIndex].MonsterName;
            hpScrollbar.size = battleSystem.MonsterBattleFactors[enemyIndex].healthPoint / (float)battleSystem.MonsterBattleFactors[enemyIndex].maxHealthPoint;
            spScrollbar.size = battleSystem.MonsterBattleFactors[enemyIndex].skillPoint / (float)battleSystem.MonsterBattleFactors[enemyIndex].maxSkillPoint;

            foreach (Transform child in statusContent)
            {
                Destroy(child.gameObject);
            }
            statusContent.sizeDelta = new Vector2(statusContent.sizeDelta.x, 60 * battleSystem.PlayerSkillEffectStatuses.Count);
            foreach (var status in battleSystem.MonstersSkillEffectStatuses[enemyIndex])
            {
                StatusBlock statusBlock = Instantiate(statusBlockPrefab);
                statusBlock.transform.SetParent(statusContent);
                statusBlock.Initial(status);
            }
        }
    }
}

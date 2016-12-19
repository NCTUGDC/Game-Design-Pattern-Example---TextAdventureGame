using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;
using System.Linq;
using TextAdventureGame.Library.General.ItemElements;

namespace TextAdventureGame.Unity.Scripts.BattleScripts
{
    public class BattlePanel : MonoBehaviour
    {
        private BattleSystem battleSystem;

        public PlayerBlock playerBlock;

        public RectTransform enemyScrollViewContent;
        public EnemyBlock enemyBlockPrefab;

        public Button normalAttackButton;
        public Button runButton;

        public RectTransform skillScrollViewContent;
        public RectTransform consumableScrollViewContent;
        public Button skillButtonPrefab;
        public Button consumableButtonPrefab;

        private int selectedEnemyIndex;

        void Start()
        {
            normalAttackButton.onClick.AddListener(NormalAttackAction);
            runButton.onClick.AddListener(RunAction);
        }

        public void Initial(BattleSystem battleSystem)
        {
            this.battleSystem = battleSystem;
            battleSystem.OnStartTurn += Render;
            battleSystem.OnEndTurn += Render;
            battleSystem.OnPlayerActionRequest += () => ShowActionButton(true);
            battleSystem.OnProcess += Render;

            battleSystem.OnStartTurn += () => 
            {
                ShowActionButton(false);
            };
            battleSystem.OnEndBattle += ClosePanel;
            battleSystem.OnRunSuccessiful += ClosePanel;

            battleSystem.StartTurn();
        }

        public void Render()
        {
            playerBlock.Render(battleSystem);

            foreach (Transform child in enemyScrollViewContent)
            {
                Destroy(child.gameObject);
            }
            enemyScrollViewContent.sizeDelta = new Vector2(260 * battleSystem.Monsters.Count, enemyScrollViewContent.sizeDelta.y);
            for(int i = 0; i < battleSystem.Monsters.Count; i++)
            {
                EnemyBlock enemyBlock = Instantiate(enemyBlockPrefab);
                enemyBlock.transform.SetParent(enemyScrollViewContent);
                enemyBlock.Render(battleSystem, i);
            }
        }

        private void NormalAttackAction()
        {
            ShowActionButton(false);
            battleSystem.NormalAttack(selectedEnemyIndex); ;
        }
        private void RunAction()
        {
            ShowActionButton(false);
            battleSystem.Run();
        }
        private void ShowActionButton(bool isShow)
        {
            if(isShow)
            {
                normalAttackButton.gameObject.SetActive(true);
                runButton.gameObject.SetActive(true);
                skillScrollViewContent.gameObject.SetActive(true);
                consumableScrollViewContent.gameObject.SetActive(true);

                foreach (Transform child in skillScrollViewContent)
                {
                    Destroy(child.gameObject);
                }
                var legalSkillIDs = battleSystem.Player.Skills.Where(x => SkillFactory.Instance.FindSkill(x).RequiredSP <= battleSystem.Player.AbilityFactors.SP);
                skillScrollViewContent.sizeDelta = new Vector2(160 * legalSkillIDs.Count(), skillScrollViewContent.sizeDelta.y);
                foreach(var skillID in legalSkillIDs)
                {
                    Skill skill = SkillFactory.Instance.FindSkill(skillID);
                    Button skillButton = Instantiate(skillButtonPrefab);
                    skillButton.transform.SetParent(skillScrollViewContent);
                    skillButton.GetComponentInChildren<Text>().text = skill.SkillName;
                    int specificSkillID = skillID;
                    int specificEnemyID = selectedEnemyIndex;
                    skillButton.onClick.AddListener(() => 
                    {
                        battleSystem.UseSkill(specificSkillID, specificEnemyID);
                    });
                }

                foreach (Transform child in consumableScrollViewContent)
                {
                    Destroy(child.gameObject);
                }
                var consumables = battleSystem.Player.Inventory.ItemInfos.Where(x => ItemFactory.Instance.FindItem(x.ItemID) is Consumable).Select<InventoryItemInfo, Consumable>(x => ItemFactory.Instance.FindItem(x.ItemID) as Consumable);
                consumableScrollViewContent.sizeDelta = new Vector2(160 * consumables.Count(), consumableScrollViewContent.sizeDelta.y);
                foreach (var consumable in consumables)
                {
                    Button consumableButton = Instantiate(consumableButtonPrefab);
                    consumableButton.transform.SetParent(consumableScrollViewContent);
                    consumableButton.GetComponentInChildren<Text>().text = consumable.ItemName;
                    int specificConsumableItemID = consumable.ItemID;
                    consumableButton.onClick.AddListener(() =>
                    {
                        battleSystem.UseItem(specificConsumableItemID);
                    });
                }
            }
            else
            {
                normalAttackButton.gameObject.SetActive(false);
                runButton.gameObject.SetActive(false);
                skillScrollViewContent.gameObject.SetActive(false);
                consumableScrollViewContent.gameObject.SetActive(false);
            }
        }

        private void ClosePanel()
        {
            battleSystem = null;
            gameObject.SetActive(false);
        }
    }
}

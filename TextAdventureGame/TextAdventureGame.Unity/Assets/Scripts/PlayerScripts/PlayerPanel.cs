using System.Text;
using TextAdventureGame.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.PlayerScripts
{
    public class PlayerPanel : MonoBehaviour
    {
        public Text playerNameText;
        public Scrollbar expScrollbar;
        public Text expText;
        public RectTransform skillScrollViewContent;
        public SkillBlock skillBlockPrefab;

        public Text levelText;
        public Text hpText;
        public Text spText;
        public Text powerText;
        public Text magicText;
        public Text agileText;
        public Text sensibilityText;
        public Text abilityPointText;
        public Button addHpButton;
        public Button addSpButton;
        public Button addPowerButton;
        public Button addMagicButton;
        public Button addAgileButton;
        public Button addSensibilityButton;

        public Text headEquipmentText;
        public Text bodyEquipmentText;
        public Text weaponEquipmentText;
        public Text footEquipmentText;
        public Text accessoryEquipmentText;
        public Button unloadHeadEquipmentButton;
        public Button unloadBodyEquipmentButton;
        public Button unloadWeaponEquipmentButton;
        public Button unloadFootEquipmentButton;
        public Button unloadAccessoryEquipmentButton;

        private void Start()
        {
            Player player = PlayerManager.Instance.Player;
            AbilityFactors ability = player.AbilityFactors;
            addHpButton.onClick.AddListener(() => { player.AbilityPoint--; ability.MaxHP += 40; ability.HP += 40; });
            addSpButton.onClick.AddListener(() => { player.AbilityPoint--; ability.MaxSP += 20; ability.SP += 20; });
            addPowerButton.onClick.AddListener(() => { player.AbilityPoint--; ability.Power++; });
            addMagicButton.onClick.AddListener(() => { player.AbilityPoint--; ability.Magic++; });
            addAgileButton.onClick.AddListener(() => { player.AbilityPoint--; ability.Agile++; });
            addSensibilityButton.onClick.AddListener(() => { player.AbilityPoint--; ability.Sensibility++; });

            unloadHeadEquipmentButton.onClick.AddListener(() => { player.Inventory.AddItem(player.HeadEquipment, 1); player.HeadEquipment = null; });
            unloadBodyEquipmentButton.onClick.AddListener(() => { player.Inventory.AddItem(player.BodyEquipment, 1); player.BodyEquipment = null; });
            unloadWeaponEquipmentButton.onClick.AddListener(() => { player.Inventory.AddItem(player.Weapon, 1); player.Weapon = null; });
            unloadFootEquipmentButton.onClick.AddListener(() => { player.Inventory.AddItem(player.FootEquipment, 1); player.FootEquipment = null; });
            unloadAccessoryEquipmentButton.onClick.AddListener(() => { player.Inventory.AddItem(player.Accessory, 1); player.Accessory = null; });

            player.OnNameChange += (value) => RenderPlayer(player);
            player.OnEXP_Change += (value) => RenderPlayer(player);
            player.OnLevelUpEXPChange += (value) => RenderPlayer(player);
            player.OnAbilityPointChange += (value) => RenderPlayer(player);

            ability.OnLevelChange += (value1, value2) => RenderPlayer(player);
            ability.OnHPChange += (value) => RenderPlayer(player);
            ability.OnMaxHPChange += (value) => RenderPlayer(player);
            ability.OnSPChange += (value) => RenderPlayer(player);
            ability.OnMaxSPChange += (value) => RenderPlayer(player);
            ability.OnPowerChange += (value) => RenderPlayer(player);
            ability.OnMagicChange += (value) => RenderPlayer(player);
            ability.OnAgileChange += (value) => RenderPlayer(player);
            ability.OnSensibilityChange += (value) => RenderPlayer(player);

            player.Inventory.OnItemChange += (value) => RenderPlayer(player);

            ability.OnLevelChange += (abilityFactors, level) =>
            {
                player.AbilityPoint += 3;
                player.LevelUpEXP = LevelEXPTable.GetLevelUpEXP(abilityFactors.Level);
                abilityFactors.MaxHP += 30;
                abilityFactors.HP += 30;
                abilityFactors.MaxSP += 15;
                abilityFactors.SP += 15;
            };

            player.OnLearnSkill += (value) => RenderPlayer(player);

            RenderPlayer(player);
        }

        public void RenderPlayer(Player player)
        {
            playerNameText.text = string.Format("玩家名稱： {0}", player.Name);
            expScrollbar.size = player.EXP / (float)player.LevelUpEXP;
            expText.text = string.Format("{0}/{1}", player.EXP, player.LevelUpEXP);
            foreach (Transform child in skillScrollViewContent)
            {
                Destroy(child.gameObject);
            }
            foreach (var skillID in player.Skills)
            {
                Skill skill = SkillFactory.Instance.FindSkill(skillID);
                SkillBlock skillBlock = Instantiate(skillBlockPrefab);
                skillBlock.transform.SetParent(skillScrollViewContent);
                skillBlock.Initial(skill);
            }

            AbilityFactors ability = player.AbilityFactors;
            levelText.text = string.Format("等級： {0}", ability.Level);
            hpText.text = string.Format("HP： {0}/{1}", ability.HP, ability.MaxHP);
            spText.text = string.Format("SP： {0}/{1}", ability.SP, ability.MaxSP);
            powerText.text = string.Format("力量： {0}", ability.Power);
            magicText.text = string.Format("魔力： {0}", ability.Magic);
            agileText.text = string.Format("敏捷： {0}", ability.Agile);
            sensibilityText.text = string.Format("感知： {0}", ability.Sensibility);
            abilityPointText.text = string.Format("能力點： {0}", player.AbilityPoint);
            if(player.AbilityPoint > 0)
            {
                addHpButton.gameObject.SetActive(true);
                addSpButton.gameObject.SetActive(true);
                addPowerButton.gameObject.SetActive(true);
                addMagicButton.gameObject.SetActive(true);
                addAgileButton.gameObject.SetActive(true);
                addSensibilityButton.gameObject.SetActive(true);
            }
            else
            {
                addHpButton.gameObject.SetActive(false);
                addSpButton.gameObject.SetActive(false);
                addPowerButton.gameObject.SetActive(false);
                addMagicButton.gameObject.SetActive(false);
                addAgileButton.gameObject.SetActive(false);
                addSensibilityButton.gameObject.SetActive(false);
            }

            if(player.HeadEquipment == null)
            {
                headEquipmentText.text = "頭部：";
                unloadHeadEquipmentButton.gameObject.SetActive(false);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("頭部：");
                stringBuilder.AppendLine(player.HeadEquipment.ItemName);
                foreach(var effector in player.HeadEquipment.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                headEquipmentText.text = stringBuilder.ToString();
                unloadHeadEquipmentButton.gameObject.SetActive(true);
            }

            if (player.BodyEquipment == null)
            {
                bodyEquipmentText.text = "身體：";
                unloadBodyEquipmentButton.gameObject.SetActive(false);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("身體：");
                stringBuilder.AppendLine(player.BodyEquipment.ItemName);
                foreach (var effector in player.BodyEquipment.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                bodyEquipmentText.text = stringBuilder.ToString();
                unloadBodyEquipmentButton.gameObject.SetActive(true);
            }

            if (player.Weapon == null)
            {
                weaponEquipmentText.text = "武器：";
                unloadWeaponEquipmentButton.gameObject.SetActive(false);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("武器：");
                stringBuilder.AppendLine(player.Weapon.ItemName);
                foreach (var effector in player.Weapon.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                weaponEquipmentText.text = stringBuilder.ToString();
                unloadWeaponEquipmentButton.gameObject.SetActive(true);
            }

            if (player.FootEquipment == null)
            {
                footEquipmentText.text = "腳部：";
                unloadFootEquipmentButton.gameObject.SetActive(false);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("腳部：");
                stringBuilder.AppendLine(player.FootEquipment.ItemName);
                foreach (var effector in player.FootEquipment.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                footEquipmentText.text = stringBuilder.ToString();
                unloadFootEquipmentButton.gameObject.SetActive(true);
            }

            if (player.Accessory == null)
            {
                accessoryEquipmentText.text = "裝飾：";
                unloadAccessoryEquipmentButton.gameObject.SetActive(false);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("裝飾：");
                stringBuilder.AppendLine(player.Accessory.ItemName);
                foreach (var effector in player.Accessory.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                accessoryEquipmentText.text = stringBuilder.ToString();
                unloadAccessoryEquipmentButton.gameObject.SetActive(true);
            }
        }
    }
}

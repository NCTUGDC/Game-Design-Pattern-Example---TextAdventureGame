using System.Text;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.ItemElements;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.InventoryScripts
{
    public class ItemBlock : MonoBehaviour
    {
        private Text itemInfoText;
        private Button useButton;

        private void Awake()
        {
            itemInfoText = transform.Find("ItemInfoText").GetComponent<Text>();
            useButton = transform.Find("UseButton").GetComponent<Button>();
        }
        public void Initial(InventoryItemInfo info)
        {
            Item item = ItemFactory.Instance.FindItem(info.ItemID);
            StringBuilder stringBuilder = new StringBuilder(string.Format("{0} ({1})\n", item.ItemName, info.Count));
            if(item is Consumable)
            {
                useButton.GetComponentInChildren<Text>().text = "使用";
                Consumable consumable = item as Consumable;
                useButton.onClick.AddListener(() => 
                {
                    if(consumable.Use(PlayerManager.Instance.Player.AbilityFactors))
                    {
                        PlayerManager.Instance.Player.Inventory.RemoveItem(consumable.ItemID, 1);
                    }
                });
                stringBuilder.Append("效果： ");
                foreach (var effector in consumable.Effectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
            }
            else if(item is Equipment)
            {
                useButton.GetComponentInChildren<Text>().text = "裝備";
                Equipment equipment = item as Equipment;
                stringBuilder.Append("數值： ");
                foreach (var effector in equipment.EquipmentEffectors)
                {
                    stringBuilder.AppendFormat("{0} ", effector.Information);
                }
                if (!equipment.IsMatchedAbilityCodition(PlayerManager.Instance.Player))
                {
                    useButton.enabled = false;
                }
                else
                {
                    useButton.onClick.AddListener(() =>
                    {
                        switch (equipment.EquipmentType)
                        {
                            case EquipmentType.Weapon:
                                if(PlayerManager.Instance.Player.Weapon != null)
                                {
                                    PlayerManager.Instance.Player.Inventory.AddItem(PlayerManager.Instance.Player.Weapon, 1);
                                }
                                PlayerManager.Instance.Player.Weapon = equipment;
                                break;
                            case EquipmentType.Head:
                                if (PlayerManager.Instance.Player.HeadEquipment != null)
                                {
                                    PlayerManager.Instance.Player.Inventory.AddItem(PlayerManager.Instance.Player.HeadEquipment, 1);
                                }
                                PlayerManager.Instance.Player.HeadEquipment = equipment;
                                break;
                            case EquipmentType.Body:
                                if (PlayerManager.Instance.Player.BodyEquipment != null)
                                {
                                    PlayerManager.Instance.Player.Inventory.AddItem(PlayerManager.Instance.Player.BodyEquipment, 1);
                                }
                                PlayerManager.Instance.Player.BodyEquipment = equipment;
                                break;
                            case EquipmentType.Foot:
                                if (PlayerManager.Instance.Player.FootEquipment != null)
                                {
                                    PlayerManager.Instance.Player.Inventory.AddItem(PlayerManager.Instance.Player.FootEquipment, 1);
                                }
                                PlayerManager.Instance.Player.FootEquipment = equipment;
                                break;
                            case EquipmentType.Accessory:
                                if (PlayerManager.Instance.Player.Accessory != null)
                                {
                                    PlayerManager.Instance.Player.Inventory.AddItem(PlayerManager.Instance.Player.Accessory, 1);
                                }
                                PlayerManager.Instance.Player.Accessory = equipment;
                                break;
                        }
                        PlayerManager.Instance.Player.Inventory.RemoveItem(equipment.ItemID, 1);
                    });
                }
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }
            itemInfoText.text = stringBuilder.ToString();
        }
    }
}

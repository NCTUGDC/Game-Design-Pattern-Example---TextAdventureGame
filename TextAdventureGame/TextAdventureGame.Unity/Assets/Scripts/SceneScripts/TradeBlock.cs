using System.Text;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.StoreElements;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class TradeBlock : MonoBehaviour
    {
        private Text rewardText;
        private Text costText;
        private Button tradeButton;

        private void Awake()
        {
            rewardText = transform.Find("RewardText").GetComponent<Text>();
            costText = transform.Find("CostText").GetComponent<Text>();
            tradeButton = transform.Find("TradeButton").GetComponent<Button>();

        }

        public void Initial(TradeInformation tradeInfo)
        {
            bool canBuy = true;
            StringBuilder rewardStringBuilder = new StringBuilder("商品：");
            foreach(var reward in tradeInfo.Rewards)
            {
                Item item = ItemFactory.Instance.FindItem(reward.itemID);
                rewardStringBuilder.AppendFormat("{0}x{1} ", item.ItemName, reward.count);
            }
            rewardText.text = rewardStringBuilder.ToString();

            StringBuilder costStringBuilder = new StringBuilder("價格：");
            foreach (var cost in tradeInfo.Costs)
            {
                Item item = ItemFactory.Instance.FindItem(cost.itemID);
                costStringBuilder.AppendFormat("{0}x{1} ", item.ItemName, cost.count);
                if(PlayerManager.Instance.Player.Inventory.ItemCount(cost.itemID) < cost.count)
                {
                    canBuy = false;
                }
            }
            costText.text = costStringBuilder.ToString();

            tradeButton.gameObject.SetActive(canBuy);
            tradeButton.onClick.AddListener(() => 
            {
                Inventory inventory = PlayerManager.Instance.Player.Inventory;
                foreach (var cost in tradeInfo.Costs)
                    inventory.RemoveItem(cost.itemID, cost.count);
                foreach (var reward in tradeInfo.Rewards)
                {
                    Item item = ItemFactory.Instance.FindItem(reward.itemID);
                    inventory.AddItem(item, reward.count);
                }
            });
        }
    }
}

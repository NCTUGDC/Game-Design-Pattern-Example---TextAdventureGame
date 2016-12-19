using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.ItemElements;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class StorePanel : MonoBehaviour
    {
        [SerializeField]
        private TradeBlock tradeBlockPrefab;
        [SerializeField]
        private RectTransform scrollViewContent;
        [SerializeField]
        private Text storeNameText;
        [SerializeField]
        private Text coinText;

        private Store store;

        private void Start()
        {
            PlayerManager.Instance.Player.Inventory.OnItemChange += UpdateStore;
            UpdateStore(null);
        }

        public void Initial(Store store)
        {
            this.store = store;
            storeNameText.text = store.StoreName;
            foreach (Transform child in scrollViewContent)
            {
                Destroy(child.gameObject);
            }

            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, 60 * store.TradeInformations.Count);
            foreach (var tradeInfo in store.TradeInformations)
            {
                TradeBlock tradeBlock = Instantiate(tradeBlockPrefab);
                tradeBlock.transform.SetParent(scrollViewContent);
                tradeBlock.Initial(tradeInfo);
            }
        }
        public void UpdateStore(InventoryItemInfo info)
        {
            Inventory inventory = PlayerManager.Instance.Player.Inventory;
            coinText.text = string.Format("錢幣數量： 金幣{0} 銀幣{1} 銅幣{2}", inventory.ItemCount(6), inventory.ItemCount(7), inventory.ItemCount(8));
            Initial(store);
        }
    }
}

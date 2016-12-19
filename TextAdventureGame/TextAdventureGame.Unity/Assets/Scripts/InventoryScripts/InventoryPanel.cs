using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.Unity.Scripts.InventoryScripts
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField]
        private ItemBlock itemBlockPrefab;
        [SerializeField]
        private RectTransform scrollViewContent;

        private void Start()
        {
            PlayerManager.Instance.Player.Inventory.OnItemChange += (info) => Initial(PlayerManager.Instance.Player.Inventory);
            Initial(PlayerManager.Instance.Player.Inventory);
        }

        public void Initial(Inventory inventory)
        {
            foreach (Transform child in scrollViewContent)
            {
                Destroy(child.gameObject);
            }
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, 100 + 100 * inventory.ItemInfos.Count()/3);
            foreach (var info in inventory.ItemInfos)
            {
                ItemBlock itemBlock = Instantiate(itemBlockPrefab);
                itemBlock.transform.SetParent(scrollViewContent);
                itemBlock.Initial(info);
            }
        }
    }
}

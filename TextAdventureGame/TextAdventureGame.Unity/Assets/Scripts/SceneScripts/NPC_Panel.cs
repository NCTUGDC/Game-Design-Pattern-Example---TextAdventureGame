using UnityEngine;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class NPC_Panel : MonoBehaviour
    {
        [SerializeField]
        private NPC_Block npcBlockPrefab;

        public void Initial(Scene scene)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var npcID in scene.NPC_IDs)
            {
                NPC_Block npcBlock = Instantiate(npcBlockPrefab);
                npcBlock.transform.SetParent(transform);
                NPC npc = NPC_Factory.Instance.FindNPC(npcID);
                npcBlock.Initial(npc);
            }
        }
    }
}

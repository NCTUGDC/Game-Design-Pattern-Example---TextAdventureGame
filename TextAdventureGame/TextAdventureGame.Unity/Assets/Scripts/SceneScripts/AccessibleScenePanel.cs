using UnityEngine;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class AccessibleScenePanel : MonoBehaviour
    {
        [SerializeField]
        private SceneBlock sceneBlockPrefab;

        public void Initial(Scene scene)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var sceneID in scene.AccessibleSceneIDs)
            {
                SceneBlock sceneBlock = Instantiate(sceneBlockPrefab);
                sceneBlock.transform.SetParent(transform);
                Scene targetScene = World.Instance.FindScene(sceneID);
                sceneBlock.Initial(targetScene);
            }
        }
    }
}

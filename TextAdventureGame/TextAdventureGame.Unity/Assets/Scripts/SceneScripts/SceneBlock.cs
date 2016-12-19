using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class SceneBlock : MonoBehaviour
    {
        private Text sceneNameText;
        private Button moveButton;

        private void Awake()
        {
            sceneNameText = transform.Find("SceneNameText").GetComponent<Text>();
            moveButton = transform.Find("MoveButton").GetComponent<Button>();
        }

        public void Initial(Scene scene)
        {
            sceneNameText.text = scene.SceneName;
            moveButton.onClick.AddListener(() => PlayerManager.Instance.Player.LocatedSceneID = scene.SceneID);
        }
    }
}

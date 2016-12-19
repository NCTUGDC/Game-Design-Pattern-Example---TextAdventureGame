using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;

namespace TextAdventureGame.Unity.Scripts.StoryScripts
{
    public class InputPlayerNameDialog : MonoBehaviour
    {
        [SerializeField]
        private InputField playerNameInputField;
        [SerializeField]
        private Button confirmButton;

        private void Start()
        {
            confirmButton.onClick.AddListener(() => 
            {
                PlayerManager.Instance.Player.Name = playerNameInputField.text;
                gameObject.SetActive(false);
            });
        }

        private void Update()
        {
            transform.SetAsLastSibling();
        }
    }
}


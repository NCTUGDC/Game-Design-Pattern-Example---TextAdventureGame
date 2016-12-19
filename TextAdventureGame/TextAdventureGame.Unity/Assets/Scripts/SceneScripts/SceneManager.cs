using UnityEngine;
using UnityEngine.UI;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.WorldElements;
using TextAdventureGame.Unity.Scripts.StoryScripts;
using TextAdventureGame.Unity.Scripts.BattleScripts;
using System.Collections.Generic;

namespace TextAdventureGame.Unity.Scripts.SceneScripts
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField]
        private GameObject scenePanel;

        [SerializeField]
        private Text sceneNameText;
        [SerializeField]
        private AccessibleScenePanel accessibleScenePanel;
        [SerializeField]
        private NPC_Panel npcPanel;
        [SerializeField]
        private StorePanel storePanel;
        [SerializeField]
        private BattlePanel battlePanel;

        [SerializeField]
        private InputPlayerNameDialog inputPlayerNameDialog;
        [SerializeField]
        private TalkDialog talkDialogPrefab;
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private Button searchButton;

        private void Start()
        {
            Instance = this;

            PlayerManager.Instance.Player.OnLocatedSceneIDChange += RenderScene;
            PlayerManager.Instance.Player.OnLocatedSceneIDChange += EnterSceneEvent;
            (InputManager.Instance as UnityInputManager).OnPlayerNameInputRequest += ShowInputPlayerNameDialog;
            searchButton.onClick.AddListener(SearchMonster);
            RenderScene(0);
        }

        // Use this for initialization
        private void RenderScene(int sceneID)
        {
            if(sceneID == 0)
            {
                scenePanel.SetActive(false);
            }
            else
            {
                scenePanel.SetActive(true);
                Scene scene = World.Instance.FindScene(sceneID);
                sceneNameText.text = scene.SceneName;
                accessibleScenePanel.Initial(scene);
                npcPanel.Initial(scene);
                if(scene.MonsterZone != null)
                {
                    searchButton.gameObject.SetActive(true);
                    searchButton.GetComponentInChildren<Text>().text = string.Format("到處走\n(遇敵率{0}%)", scene.MonsterZone.EncounterProbability);
                }
                else
                {
                    searchButton.gameObject.SetActive(false);
                }
            }
        }
        private void EnterSceneEvent(int sceneID)
        {
            SentenceManager.Instance.ToNext();
        }
        private void ShowInputPlayerNameDialog()
        {
            inputPlayerNameDialog.gameObject.SetActive(true);
            inputPlayerNameDialog.gameObject.transform.SetAsLastSibling();
        }
        public void OpenStore(int storeID)
        {
            storePanel.gameObject.SetActive(true);
            storePanel.Initial(StoreFactory.Instance.FindStore(storeID));
        }
        public void TalkWithNPC(NPC npc)
        {
            InputManager.Instance.TalkingNPC_ID = npc.NPC_ID;
            TalkDialog dialog = Instantiate(talkDialogPrefab);
            dialog.transform.SetParent(canvas.transform);
            dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            dialog.Initial(npc);
            SentenceManager.Instance.ToNext();
        }
        private void SearchMonster()
        {
            Scene scene = World.Instance.FindScene(PlayerManager.Instance.Player.LocatedSceneID);
            var monsterTeam = scene.MonsterZone.GetMonsterTeam();
            if (scene.MonsterZone == null || monsterTeam == null)
            {
                TalkDialog dialog = Instantiate(talkDialogPrefab);
                dialog.transform.SetParent(canvas.transform);
                dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                dialog.Initial(new NPC(0, "系統", "什麼都沒找到"));
            }
            else
            {
                List<Monster> monsters = new List<Monster>();
                monsterTeam.ForEach(x => monsters.Add(MonsterFactory.Instance.FindMonster(x)));
                battlePanel.gameObject.SetActive(true);
                battlePanel.Initial(new BattleSystem(PlayerManager.Instance.Player, monsters));
                TalkDialog dialog = Instantiate(talkDialogPrefab);
                dialog.transform.SetParent(canvas.transform);
                dialog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                dialog.Initial(new NPC(0, "系統", "戰鬥"));
            }
        }
    }
}

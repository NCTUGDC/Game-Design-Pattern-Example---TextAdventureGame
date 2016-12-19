using TextAdventureGame.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.BattleScripts
{
    public class StatusBlock : MonoBehaviour
    {
        private Text text;

        void Start()
        {
            text = GetComponentInChildren<Text>();
        }

        public void Initial(BattleSystem.SkillEffectStatus status)
        {
            text.text = string.Format("{0} 剩餘{1}回合", status.effector.Information, status.remainedRound);
        }
    }
}

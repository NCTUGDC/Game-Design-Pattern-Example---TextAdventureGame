using System.Text;
using TextAdventureGame.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace TextAdventureGame.Unity.Scripts.PlayerScripts
{
    public class SkillBlock : MonoBehaviour
    {
        private Text skillInfoText;

        private void Awake()
        {
            skillInfoText = transform.Find("SkillInfoText").GetComponent<Text>();
        }

        public void Initial(Skill skill)
        {
            StringBuilder stringBuilder = new StringBuilder(string.Format("{0} 消耗SP{1}", skill.SkillName, skill.RequiredSP));
            if(skill.IsAoE)
            {
                stringBuilder.Append("(全體)");
            }
            stringBuilder.AppendLine();
            foreach(var effector in skill.SkillEffectors)
            {
                stringBuilder.AppendFormat(string.Format("{0} ", effector.Information));
            }
            skillInfoText.text = stringBuilder.ToString();
        }
    }
}

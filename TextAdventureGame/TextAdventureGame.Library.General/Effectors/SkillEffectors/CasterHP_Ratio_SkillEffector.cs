using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterHP_Ratio_SkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "Ratio")]
        public float Ratio { get; private set; }

        public override string Information
        {
            get
            {
                if (Ratio > 0)
                    return string.Format("施放者回復HP {0}%", (int)(Ratio * 100));
                else
                    return string.Format("施放者消耗HP {0}%", (int)(-Ratio * 100));
            }
        }

        public CasterHP_Ratio_SkillEffector() { }
        public CasterHP_Ratio_SkillEffector(float ratio)
        {
            Ratio = ratio;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.healthPoint += (int)(casterFactors.maxHealthPoint * Ratio);
        }
    }
}

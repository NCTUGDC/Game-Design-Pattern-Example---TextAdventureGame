using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterSP_Ratio_SkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "Ratio")]
        public float Ratio { get; private set; }

        public override string Information
        {
            get
            {
                if (Ratio > 0)
                    return string.Format("施放者回復SP {0}%", (int)(Ratio * 100));
                else
                    return string.Format("施放者消耗SP {0}%", (int)(-Ratio * 100));
            }
        }

        public CasterSP_Ratio_SkillEffector() { }
        public CasterSP_Ratio_SkillEffector(float ratio)
        {
            Ratio = ratio;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.skillPoint += (int)(casterFactors.maxSkillPoint * Ratio);
        }
    }
}

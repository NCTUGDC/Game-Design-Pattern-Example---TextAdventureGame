using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterSP_SkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "SP")]
        public int SP { get; private set; }

        public override string Information
        {
            get
            {
                if (SP > 0)
                    return string.Format("施放者回復SP{0}點", SP);
                else
                    return string.Format("施放者失去SP{0}點", -SP);
            }
        }

        public CasterSP_SkillEffector() { }
        public CasterSP_SkillEffector(int sp)
        {
            SP = sp;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.skillPoint += SP;
        }
    }
}

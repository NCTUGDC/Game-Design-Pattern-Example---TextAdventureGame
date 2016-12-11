using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterSP_SkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "SP")]
        public int SP { get; private set; }

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

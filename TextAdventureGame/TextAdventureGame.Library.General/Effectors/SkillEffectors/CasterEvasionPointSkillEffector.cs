using MsgPack.Serialization;
using System.Collections.Generic;
using System;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterEvasionPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "EvasionPoint")]
        public int EvasionPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (EvasionPoint > 0)
                    return string.Format("施放者迴避提升{0}點{1}回合", EvasionPoint, SustainRound);
                else
                    return string.Format("施放者迴避下降{0}點{1}回合", -EvasionPoint, SustainRound);
            }
        }

        public CasterEvasionPointSkillEffector() { }
        public CasterEvasionPointSkillEffector(int sustainRound, int evasionPoint) : base(sustainRound)
        {
            EvasionPoint = evasionPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.evasionPoint += EvasionPoint;
        }

        public override void End(BattleFactors affectedBF)
        {
            affectedBF.evasionPoint -= EvasionPoint;
        }
    }
}

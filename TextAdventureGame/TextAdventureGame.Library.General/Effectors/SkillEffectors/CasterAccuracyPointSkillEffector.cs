using MsgPack.Serialization;
using System.Collections.Generic;
using System;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterAccuracyPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "AccuracyPoint")]
        public int AccuracyPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (AccuracyPoint > 0)
                    return string.Format("施放者命中提升{0}點{1}回合", AccuracyPoint, SustainRound);
                else
                    return string.Format("施放者命中下降{0}點{1}回合", -AccuracyPoint, SustainRound);
            }
        }

        public CasterAccuracyPointSkillEffector() { }
        public CasterAccuracyPointSkillEffector(int sustainRound, int accuracyPoint) : base(sustainRound)
        {
            AccuracyPoint = accuracyPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.accuracyPoint += AccuracyPoint;
        }

        public override void End(BattleFactors affectedBF)
        {
            affectedBF.accuracyPoint -= AccuracyPoint;
        }
    }
}

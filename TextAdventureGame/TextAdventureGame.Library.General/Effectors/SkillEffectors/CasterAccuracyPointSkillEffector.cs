using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterAccuracyPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "AccuracyPoint")]
        public int AccuracyPoint { get; private set; }

        public CasterAccuracyPointSkillEffector(int sustainRound, int accuracyPoint) : base(sustainRound)
        {
            AccuracyPoint = accuracyPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.accuracyPoint += AccuracyPoint;
        }
    }
}

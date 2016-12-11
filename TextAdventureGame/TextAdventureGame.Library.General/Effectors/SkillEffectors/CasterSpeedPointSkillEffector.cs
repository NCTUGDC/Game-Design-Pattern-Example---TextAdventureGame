using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterSpeedPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "SpeedPoint")]
        public int SpeedPoint { get; private set; }

        public CasterSpeedPointSkillEffector(int sustainRound, int speedPoint) : base(sustainRound)
        {
            SpeedPoint = speedPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.speedPoint += SpeedPoint;
        }
    }
}

using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterPhysicalDefencePointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "PhysicalDefencePoint")]
        public int PhysicalDefencePoint { get; private set; }

        public override string Information
        {
            get
            {
                if (PhysicalDefencePoint > 0)
                    return string.Format("施放者物防提升{0}點{1}回合", PhysicalDefencePoint, SustainRound);
                else
                    return string.Format("施放者物防下降{0}點{1}回合", -PhysicalDefencePoint, SustainRound);
            }
        }

        public CasterPhysicalDefencePointSkillEffector() { }
        public CasterPhysicalDefencePointSkillEffector(int sustainRound, int physicalDefencePoint) : base(sustainRound)
        {
            PhysicalDefencePoint = physicalDefencePoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.physicalDefencePoint += PhysicalDefencePoint;
        }
        public override void End(BattleFactors affectedBF)
        {
            affectedBF.physicalDefencePoint -= PhysicalDefencePoint;
        }
    }
}

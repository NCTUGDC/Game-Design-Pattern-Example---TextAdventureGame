using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterPhysicalDefencePointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "PhysicalDefencePoint")]
        public int PhysicalDefencePoint { get; private set; }

        public CasterPhysicalDefencePointSkillEffector(int sustainRound, int physicalDefencePoint) : base(sustainRound)
        {
            PhysicalDefencePoint = physicalDefencePoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.physicalDefencePoint += PhysicalDefencePoint;
        }
    }
}

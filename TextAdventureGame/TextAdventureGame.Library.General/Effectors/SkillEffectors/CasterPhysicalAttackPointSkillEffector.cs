using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterPhysicalAttackPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "PhysicalAttackPoint")]
        public int PhysicalAttackPoint { get; private set; }

        public CasterPhysicalAttackPointSkillEffector(int sustainRound, int physicalAttackPoint) : base(sustainRound)
        {
            PhysicalAttackPoint = physicalAttackPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.physicalAttackPoint += PhysicalAttackPoint;
        }
    }
}

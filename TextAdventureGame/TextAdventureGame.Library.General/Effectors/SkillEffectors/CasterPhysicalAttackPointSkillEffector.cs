using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterPhysicalAttackPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "PhysicalAttackPoint")]
        public int PhysicalAttackPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (PhysicalAttackPoint > 0)
                    return string.Format("施放者物攻提升{0}點{1}回合", PhysicalAttackPoint, SustainRound);
                else
                    return string.Format("施放者物攻下降{0}點{1}回合", -PhysicalAttackPoint, SustainRound);
            }
        }

        public CasterPhysicalAttackPointSkillEffector() { }
        public CasterPhysicalAttackPointSkillEffector(int sustainRound, int physicalAttackPoint) : base(sustainRound)
        {
            PhysicalAttackPoint = physicalAttackPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.physicalAttackPoint += PhysicalAttackPoint;
        }
        public override void End(BattleFactors affectedBF)
        {
            affectedBF.physicalAttackPoint -= PhysicalAttackPoint;
        }
    }
}

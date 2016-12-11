using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class PhysicalAttackSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "PhysicalAttack")]
        public int PhysicalAttack { get; private set; }

        public PhysicalAttackSkillEffector(int physicalAttack)
        {
            PhysicalAttack = physicalAttack;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            int physicalAttackPoint = casterFactors.physicalAttackPoint + PhysicalAttack;
            foreach(var factors in targetsFactors)
            {
                factors.healthPoint -= physicalAttackPoint - factors.physicalDefencePoint;
            }
        }
    }
}

using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class PhysicalAttackSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "PhysicalAttack")]
        public int PhysicalAttack { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("物攻{0}點", PhysicalAttack);
            }
        }

        public PhysicalAttackSkillEffector() { }
        public PhysicalAttackSkillEffector(int physicalAttack)
        {
            PhysicalAttack = physicalAttack;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            int physicalAttackPoint = casterFactors.physicalAttackPoint + PhysicalAttack;
            foreach(var factors in targetsFactors)
            {
                factors.healthPoint -= Math.Max(physicalAttackPoint - factors.physicalDefencePoint, 1);
            }
        }
    }
}

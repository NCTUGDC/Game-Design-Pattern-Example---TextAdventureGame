using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class MagicalAttackSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalAttack")]
        public int MagicalAttack { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("魔攻{0}點", MagicalAttack);
            }
        }

        public MagicalAttackSkillEffector() { }
        public MagicalAttackSkillEffector(int magicalAttack)
        {
            MagicalAttack = magicalAttack;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            int magicalAttackPoint = casterFactors.magicalAttackPoint + MagicalAttack;
            foreach (var factors in targetsFactors)
            {
                factors.healthPoint -= Math.Max(magicalAttackPoint - factors.magicalDefencePoint, 1);
            }
        }
    }
}

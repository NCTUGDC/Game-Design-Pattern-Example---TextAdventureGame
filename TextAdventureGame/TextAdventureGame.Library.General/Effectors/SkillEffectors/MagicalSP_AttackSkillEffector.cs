using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class MagicalSP_AttackSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalAttack")]
        public int MagicalAttack { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("魔攻{0}點 以對象SP作為傷害計算", MagicalAttack);
            }
        }

        public MagicalSP_AttackSkillEffector() { }
        public MagicalSP_AttackSkillEffector(int magicalAttack)
        {
            MagicalAttack = magicalAttack;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            int magicalAttackPoint = casterFactors.magicalAttackPoint + MagicalAttack;
            foreach (var factors in targetsFactors)
            {
                factors.skillPoint -= Math.Max(magicalAttackPoint - factors.magicalDefencePoint, 1);
            }
        }
    }
}

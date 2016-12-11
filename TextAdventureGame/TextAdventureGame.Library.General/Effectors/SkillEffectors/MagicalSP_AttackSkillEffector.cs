using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class MagicalSP_AttackSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalAttack")]
        public int MagicalAttack { get; private set; }

        public MagicalSP_AttackSkillEffector(int magicalAttack)
        {
            MagicalAttack = magicalAttack;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            int magicalAttackPoint = casterFactors.magicalAttackPoint + MagicalAttack;
            foreach (var factors in targetsFactors)
            {
                factors.skillPoint -= magicalAttackPoint - factors.magicalDefencePoint;
            }
        }
    }
}

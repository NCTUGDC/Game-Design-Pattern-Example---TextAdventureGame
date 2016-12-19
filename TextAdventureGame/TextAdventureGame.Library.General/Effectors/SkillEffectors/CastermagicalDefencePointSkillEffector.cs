using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterMagicalDefencePointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "MagicalDefencePoint")]
        public int MagicalDefencePoint { get; private set; }

        public override string Information
        {
            get
            {
                if (MagicalDefencePoint > 0)
                    return string.Format("施放者魔防提升{0}點{1}回合", MagicalDefencePoint, SustainRound);
                else
                    return string.Format("施放者魔防下降{0}點{1}回合", -MagicalDefencePoint, SustainRound);
            }
        }

        public CasterMagicalDefencePointSkillEffector() { }
        public CasterMagicalDefencePointSkillEffector(int sustainRound, int magicalDefencePoint) : base(sustainRound)
        {
            MagicalDefencePoint = magicalDefencePoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.magicalDefencePoint += MagicalDefencePoint;
        }
        public override void End(BattleFactors affectedBF)
        {
            affectedBF.magicalDefencePoint -= MagicalDefencePoint;
        }
    }
}

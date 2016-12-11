using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterMagicalDefencePointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "MagicalDefencePoint")]
        public int MagicalDefencePoint { get; private set; }

        public CasterMagicalDefencePointSkillEffector(int sustainRound, int magicalDefencePoint) : base(sustainRound)
        {
            MagicalDefencePoint = magicalDefencePoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.magicalDefencePoint += MagicalDefencePoint;
        }
    }
}

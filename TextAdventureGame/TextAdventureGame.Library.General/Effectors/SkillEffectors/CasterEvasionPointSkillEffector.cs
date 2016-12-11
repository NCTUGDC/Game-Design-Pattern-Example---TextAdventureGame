using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class CasterEvasionPointSkillEffector : SustainSkillEffector
    {
        [MessagePackMember(id: 1, Name = "EvasionPoint")]
        public int EvasionPoint { get; private set; }

        public CasterEvasionPointSkillEffector(int sustainRound, int evasionPoint) : base(sustainRound)
        {
            EvasionPoint = evasionPoint;
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {
            casterFactors.evasionPoint += EvasionPoint;
        }
    }
}

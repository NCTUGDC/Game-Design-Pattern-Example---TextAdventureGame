using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class TargetStopActionSkillEffector : SustainSkillEffector
    {
        public TargetStopActionSkillEffector(int sustainRound) : base(sustainRound)
        {
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {

        }
    }
}

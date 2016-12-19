using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public class TargetStopActionSkillEffector : SustainSkillEffector
    {
        public override string Information
        {
            get
            {
                return string.Format("對象停止行動{0}回合", SustainRound);
            }
        }

        public TargetStopActionSkillEffector() { }
        public TargetStopActionSkillEffector(int sustainRound) : base(sustainRound)
        {
        }

        public override void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors)
        {

        }
        public override void End(BattleFactors affectedBF)
        {

        }
    }
}

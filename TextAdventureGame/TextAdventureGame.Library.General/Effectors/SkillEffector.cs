using System.Collections.Generic;

namespace TextAdventureGame.Library.General.Effectors
{
    public abstract class SkillEffector
    {
        public abstract void Use(BattleFactors casterFactors, List<BattleFactors> targetsFactors);
    }
}

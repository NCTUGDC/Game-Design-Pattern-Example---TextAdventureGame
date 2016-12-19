namespace TextAdventureGame.Library.General.Effectors
{
    public abstract class AbilityConditionEffector
    {
        public abstract string Information { get; }
        public abstract bool IsSufficient(AbilityFactors abilityFactors);
    }
}

namespace TextAdventureGame.Library.General.Effectors
{
    public abstract class ConsumableEffector
    {
        public abstract string Information { get; }
        public abstract bool Affect(AbilityFactors abilityFactors);
    }
}

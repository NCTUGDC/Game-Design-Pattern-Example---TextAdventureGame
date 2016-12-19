namespace TextAdventureGame.Library.General.Effectors
{
    public abstract class EquipmentEffector
    {
        public abstract string Information { get; }
        public abstract BattleFactors Use(BattleFactors battleFactors);
    }
}

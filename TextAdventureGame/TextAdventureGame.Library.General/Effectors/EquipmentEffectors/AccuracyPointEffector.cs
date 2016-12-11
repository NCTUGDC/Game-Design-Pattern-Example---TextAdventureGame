using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class AccuracyPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "AccuracyPoint")]
        public int AccuracyPoint { get; private set; }

        public AccuracyPointEffector(int accuracyPoint)
        {
            AccuracyPoint = accuracyPoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.accuracyPoint += AccuracyPoint;
            return battleFactors;
        }
    }
}

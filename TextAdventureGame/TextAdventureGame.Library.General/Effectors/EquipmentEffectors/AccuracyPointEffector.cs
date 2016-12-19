using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class AccuracyPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "AccuracyPoint")]
        public int AccuracyPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (AccuracyPoint > 0)
                    return string.Format("命中+{0}", AccuracyPoint);
                else
                    return string.Format("命中-{0}", -AccuracyPoint);
            }
        }

        public AccuracyPointEffector() { }
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

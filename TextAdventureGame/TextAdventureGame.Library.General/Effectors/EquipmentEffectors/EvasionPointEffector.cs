using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class EvasionPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "EvasionPoint")]
        public int EvasionPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (EvasionPoint > 0)
                    return string.Format("迴避+{0}", EvasionPoint);
                else
                    return string.Format("迴避-{0}", -EvasionPoint);
            }
        }

        public EvasionPointEffector() { }
        public EvasionPointEffector(int evasionPoint)
        {
            EvasionPoint = evasionPoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.evasionPoint += EvasionPoint;
            return battleFactors;
        }
    }
}

using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class EvasionPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "EvasionPoint")]
        public int EvasionPoint { get; private set; }

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

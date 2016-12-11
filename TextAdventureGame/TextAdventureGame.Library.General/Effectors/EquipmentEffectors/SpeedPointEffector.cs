using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class SpeedPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "SpeedPoint")]
        public int SpeedPoint { get; private set; }

        public SpeedPointEffector(int speedPoint)
        {
            SpeedPoint = speedPoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.speedPoint += SpeedPoint;
            return battleFactors;
        }
    }
}

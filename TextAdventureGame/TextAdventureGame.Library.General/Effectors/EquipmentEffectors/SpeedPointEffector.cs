using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class SpeedPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "SpeedPoint")]
        public int SpeedPoint { get; private set; }

        public override string Information
        {
            get
            {
                if(SpeedPoint > 0)
                    return string.Format("速度+{0}", SpeedPoint);
                else
                    return string.Format("速度-{0}", -SpeedPoint);
            }
        }
        public SpeedPointEffector() { }
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

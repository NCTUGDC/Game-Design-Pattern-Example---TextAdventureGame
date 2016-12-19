using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class PhysicalDefencePointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "PhysicalDefencePoint")]
        public int PhysicalDefencePoint { get; private set; }

        public override string Information
        {
            get
            {
                if (PhysicalDefencePoint > 0)
                    return string.Format("物防+{0}", PhysicalDefencePoint);
                else
                    return string.Format("物防-{0}", -PhysicalDefencePoint);
            }
        }
        public PhysicalDefencePointEffector() { }
        public PhysicalDefencePointEffector(int physicalDefencePoint)
        {
            PhysicalDefencePoint = physicalDefencePoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.physicalDefencePoint += PhysicalDefencePoint;
            return battleFactors;
        }
    }
}

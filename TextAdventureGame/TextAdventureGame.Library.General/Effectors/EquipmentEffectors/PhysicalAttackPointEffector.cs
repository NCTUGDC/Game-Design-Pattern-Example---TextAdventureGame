using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class PhysicalAttackPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "PhysicalAttackPoint")]
        public int PhysicalAttackPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (PhysicalAttackPoint > 0)
                    return string.Format("物攻+{0}", PhysicalAttackPoint);
                else
                    return string.Format("物攻-{0}", -PhysicalAttackPoint);
            }
        }
        public PhysicalAttackPointEffector() { }
        public PhysicalAttackPointEffector(int physicalAttackPoint)
        {
            PhysicalAttackPoint = physicalAttackPoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.magicalAttackPoint += PhysicalAttackPoint;
            return battleFactors;
        }
    }
}

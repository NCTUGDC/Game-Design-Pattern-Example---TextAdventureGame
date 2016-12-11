using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    class PhysicalAttackPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "PhysicalAttackPoint")]
        public int PhysicalAttackPoint { get; private set; }

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

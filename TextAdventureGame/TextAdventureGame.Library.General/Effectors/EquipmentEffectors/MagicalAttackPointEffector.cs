using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class MagicalAttackPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalAttackPoint")]
        public int MagicalAttackPoint { get; private set; }

        public MagicalAttackPointEffector(int magicalAttackPoint)
        {
            MagicalAttackPoint = magicalAttackPoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.magicalAttackPoint += MagicalAttackPoint;
            return battleFactors;
        }
    }
}

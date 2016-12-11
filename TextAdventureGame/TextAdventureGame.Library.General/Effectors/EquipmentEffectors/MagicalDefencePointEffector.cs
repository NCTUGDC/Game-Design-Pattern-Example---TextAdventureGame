using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class MagicalDefencePointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalDefencePoint")]
        public int MagicalDefencePoint { get; private set; }

        public MagicalDefencePointEffector(int magicalDefencePoint)
        {
            MagicalDefencePoint = magicalDefencePoint;
        }

        public override BattleFactors Use(BattleFactors battleFactors)
        {
            battleFactors.magicalDefencePoint += MagicalDefencePoint;
            return battleFactors;
        }
    }
}

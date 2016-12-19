using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class MagicalDefencePointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalDefencePoint")]
        public int MagicalDefencePoint { get; private set; }

        public override string Information
        {
            get
            {
                if (MagicalDefencePoint > 0)
                    return string.Format("魔防+{0}", MagicalDefencePoint);
                else
                    return string.Format("魔防-{0}", -MagicalDefencePoint);
            }
        }

        public MagicalDefencePointEffector() { }
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

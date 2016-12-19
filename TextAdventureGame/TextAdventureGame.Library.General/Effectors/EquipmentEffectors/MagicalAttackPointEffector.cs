using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.EquipmentEffectors
{
    public class MagicalAttackPointEffector : EquipmentEffector
    {
        [MessagePackMember(id: 0, Name = "MagicalAttackPoint")]
        public int MagicalAttackPoint { get; private set; }

        public override string Information
        {
            get
            {
                if (MagicalAttackPoint > 0)
                    return string.Format("魔攻+{0}", MagicalAttackPoint);
                else
                    return string.Format("魔攻-{0}", -MagicalAttackPoint);
            }
        }

        public MagicalAttackPointEffector() { }
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

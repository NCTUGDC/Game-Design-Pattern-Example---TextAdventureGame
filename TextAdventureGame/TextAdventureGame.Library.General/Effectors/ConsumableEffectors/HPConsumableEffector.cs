using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.ConsumableEffectors
{
    public class HPConsumableEffector : ConsumableEffector
    {
        [MessagePackMember(id: 0, Name = "HP")]
        public int HP { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("HP+{0}", HP);
            }
        }

        public HPConsumableEffector() { }
        public HPConsumableEffector(int hp)
        {
            HP = hp;
        }

        public override bool Affect(AbilityFactors abilityFactors)
        {
            abilityFactors.HP += HP;
            return true;
        }
    }
}

using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.ConsumableEffectors
{
    public class SPConsumableEffector : ConsumableEffector
    {
        [MessagePackMember(id: 0, Name = "SP")]
        public int SP { get; private set; }

        public SPConsumableEffector(int sp)
        {
            SP = sp;
        }

        public override bool Affect(AbilityFactors abilityFactors)
        {
            abilityFactors.SP += SP;
            return true;
        }
    }
}

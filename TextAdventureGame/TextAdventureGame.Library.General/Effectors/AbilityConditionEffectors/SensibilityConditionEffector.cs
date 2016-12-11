using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class SensibilityConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Sensibility")]
        public int Sensibility { get; private set; }

        public SensibilityConditionEffector(int sensibility)
        {
            Sensibility = sensibility;
        }

        public override bool IsSufficient(AbilityFactors abilityFactors)
        {
            return abilityFactors.Magic >= Sensibility;
        }
    }
}

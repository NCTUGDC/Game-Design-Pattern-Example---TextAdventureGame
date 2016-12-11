using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class AgileConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Agile")]
        public int Agile { get; private set; }

        public AgileConditionEffector(int agile)
        {
            Agile = agile;
        }

        public override bool IsSufficient(AbilityFactors abilityFactors)
        {
            return abilityFactors.Magic >= Agile;
        }
    }
}

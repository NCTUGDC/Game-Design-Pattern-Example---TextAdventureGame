using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class AgileConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Agile")]
        public int Agile { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("敏捷：{0}", Agile);
            }
        }

        public AgileConditionEffector() { }
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

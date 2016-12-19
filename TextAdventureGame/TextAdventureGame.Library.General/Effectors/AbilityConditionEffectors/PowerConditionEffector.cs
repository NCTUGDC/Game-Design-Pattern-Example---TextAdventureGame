using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class PowerConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Power")]
        public int Power { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("力量：{0}", Power);
            }
        }

        public PowerConditionEffector() { }
        public PowerConditionEffector(int power)
        {
            Power = power;
        }

        public override bool IsSufficient(AbilityFactors abilityFactors)
        {
            return abilityFactors.Power >= Power;
        }
    }
}

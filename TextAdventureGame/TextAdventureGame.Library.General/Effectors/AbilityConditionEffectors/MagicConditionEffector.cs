using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class MagicConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Magic")]
        public int Magic { get; private set; }

        public override string Information
        {
            get
            {
                return string.Format("魔力：{0}", Magic);
            }
        }

        public MagicConditionEffector() { }
        public MagicConditionEffector(int magic)
        {
            Magic = magic;
        }

        public override bool IsSufficient(AbilityFactors abilityFactors)
        {
            return abilityFactors.Magic >= Magic;
        }
    }
}

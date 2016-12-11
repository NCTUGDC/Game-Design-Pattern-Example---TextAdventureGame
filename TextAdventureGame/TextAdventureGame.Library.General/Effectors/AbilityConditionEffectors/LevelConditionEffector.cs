using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors
{
    public class LevelConditionEffector : AbilityConditionEffector
    {
        [MessagePackMember(id: 0, Name = "Level")]
        public int Level { get; private set; }

        public LevelConditionEffector(int level)
        {
            Level = level;
        }

        public override bool IsSufficient(AbilityFactors abilityFactors)
        {
            return abilityFactors.Level >= Level;
        }
    }
}

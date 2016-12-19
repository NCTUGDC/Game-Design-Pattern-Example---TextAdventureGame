using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public abstract class SustainSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "SustainRound")]
        public int SustainRound { get; private set; }

        public SustainSkillEffector() { }
        protected SustainSkillEffector(int sustainRound)
        {
            SustainRound = sustainRound;
        }

        public abstract void End(BattleFactors affectedBF);
    }
}

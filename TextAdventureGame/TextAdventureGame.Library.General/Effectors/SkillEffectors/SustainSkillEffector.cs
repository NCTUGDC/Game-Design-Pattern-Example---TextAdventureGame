using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.Effectors.SkillEffectors
{
    public abstract class SustainSkillEffector : SkillEffector
    {
        [MessagePackMember(id: 0, Name = "SustainRound")]
        public int SustainRound { get; private set; }

        protected SustainSkillEffector(int sustainRound)
        {
            SustainRound = sustainRound;
        }
    }
}

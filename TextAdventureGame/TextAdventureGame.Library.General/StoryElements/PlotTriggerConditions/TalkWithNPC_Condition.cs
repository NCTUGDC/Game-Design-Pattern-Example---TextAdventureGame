using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerConditions
{
    public class TalkWithNPC_Condition : PlotTriggerCondition
    {
        [MessagePackMember(id: 1, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }

        public override string ConditionInformation
        {
            get
            {
                return string.Format("與NPC交談 NPC ID: {0}", NPC_ID);
            }
        }

        public TalkWithNPC_Condition(int conditionID, int npcID) : base(conditionID)
        {
            NPC_ID = npcID;
        }

        public override bool IsEligible()
        {
            return InputManager.Instance.TalkingNPC_ID == NPC_ID;
        }
    }
}

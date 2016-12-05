using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class ChangeNPC_ConversationContentEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }
        [MessagePackMember(id: 2, Name = "ConversationContent")]
        public string ConversationContent { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("變更NPC對話內容 NPC ID: {0}, 內容： {1}", NPC_ID, ConversationContent);
            }
        }

        public ChangeNPC_ConversationContentEvent(int endEventID, int npcID, string conversationContent) : base(endEventID)
        {
            NPC_ID = npcID;
            ConversationContent = conversationContent;
        }

        public override void Execute()
        {
            NPC_Factory.Instance.FindNPC(NPC_ID).ConversationContent = ConversationContent;
        }
    }
}

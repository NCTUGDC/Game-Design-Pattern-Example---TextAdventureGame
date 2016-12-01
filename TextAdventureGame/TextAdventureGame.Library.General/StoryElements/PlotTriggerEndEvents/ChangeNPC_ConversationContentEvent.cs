using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class ChangeNPC_ConversationContentEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "SceneID")]
        public int SceneID { get; private set; }
        [MessagePackMember(id: 2, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }
        [MessagePackMember(id: 3, Name = "ConversationContent")]
        public string ConversationContent { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("變更NPC對話內容 SceneID： {0}, NPC ID: {1}, 內容： {2}", SceneID, NPC_ID, ConversationContent);
            }
        }

        public ChangeNPC_ConversationContentEvent(int endEventID, int sceneID, int npcID, string conversationContent) : base(endEventID)
        {
            SceneID = sceneID;
            NPC_ID = npcID;
            ConversationContent = conversationContent;
        }

        public override void Execute()
        {
            World.Instance.FindScene(SceneID).FindNPC(NPC_ID).ConversationContent = ConversationContent;
        }
    }
}

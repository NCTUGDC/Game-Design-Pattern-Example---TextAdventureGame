using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class RemoveNPC_Event : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "SceneID")]
        public int SceneID { get; private set; }
        [MessagePackMember(id: 2, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("移除NPC SceneID： {0}, NPC ID: {1}", SceneID, NPC_ID);
            }
        }

        public RemoveNPC_Event(int endEventID, int sceneID, int npcID) : base(endEventID)
        {
            SceneID = sceneID;
            NPC_ID = npcID;
        }

        public override void Execute()
        {
            World.Instance.FindScene(SceneID).RemoveNPC(NPC_ID);
        }
    }
}

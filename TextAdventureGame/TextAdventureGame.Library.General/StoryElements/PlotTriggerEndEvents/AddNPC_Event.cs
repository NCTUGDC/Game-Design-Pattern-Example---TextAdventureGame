using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class AddNPC_Event : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "SceneID")]
        public int SceneID { get; private set; }
        [MessagePackMember(id: 2, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("加入NPC SceneID： {0}, NPC ID: {1}", SceneID, NPC_ID);
            }
        }

        public AddNPC_Event(int endEventID, int sceneID, int npcID) : base(endEventID)
        {
            SceneID = sceneID;
            NPC_ID = npcID;
        }

        public override void Execute()
        {
            World.Instance.FindScene(SceneID).AddNPC_ID(NPC_ID);
        }
    }
}

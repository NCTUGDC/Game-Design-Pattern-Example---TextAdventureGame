using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class ChangeSceneEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "SceneID")]
        public int SceneID { get; private set; }
        public override string EventInformation
        {
            get
            {
                return string.Format("更改場景 場景ID： {0}", SceneID);
            }
        }
        public ChangeSceneEvent() { }
        public ChangeSceneEvent(int endEventID, int sceneID) : base(endEventID)
        {
            SceneID = sceneID;
        }

        public override void Execute()
        {
            PlayerManager.Instance.Player.LocatedSceneID = SceneID;
        }
    }
}

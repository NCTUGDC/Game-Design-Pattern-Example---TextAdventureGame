using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoryElements
{
    public abstract class PlotTriggerEvent
    {
        [MessagePackMember(id: 0, Name = "EventID")]
        public int EventID { get; private set; }
        public abstract string EventInformation { get; }

        public PlotTriggerEvent() { }
        protected PlotTriggerEvent(int endEventID)
        {
            EventID = endEventID;
        }
        public abstract void Execute();
    }
}

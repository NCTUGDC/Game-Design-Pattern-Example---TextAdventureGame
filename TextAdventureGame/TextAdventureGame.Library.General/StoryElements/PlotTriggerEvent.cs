using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements
{
    public abstract class PlotTriggerEvent : IInputActionCallbackTarget
    {
        [MessagePackMember(id: 0, Name = "EventID")]
        public int EventID { get; private set; }
        public abstract string EventInformation { get; }
        [MessagePackIgnore]
        public bool IsCompleted { get; protected set; }

        protected PlotTriggerEvent(int endEventID)
        {
            EventID = endEventID;
            IsCompleted = false;
        }
        public virtual bool Execute(List<object> informationProviders)
        {
            return informationProviders != null;
        }
        public virtual bool Response(List<object> informationProviders)
        {
            return informationProviders != null;
        }
    }
}

using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public abstract class InputStringEvent : PlotTriggerEvent, IInputActionCallbackTarget
    {
        protected InputStringEvent(int endEventID) : base(endEventID)
        {
        }

        public override void Execute()
        {
            InputManager.Instance.InputStringRequest(this);
        }
        public abstract void Response(Dictionary<byte, object> parameters);
    }
}

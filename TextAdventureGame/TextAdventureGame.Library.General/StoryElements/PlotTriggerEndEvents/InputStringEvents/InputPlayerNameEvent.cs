using System.Collections.Generic;
using TextAdventureGame.Library.General.Protocols;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents.InputStringEvents
{
    public class InputPlayerNameEvent : InputStringEvent
    {
        public override string EventInformation
        {
            get
            {
                return "輸入玩家名稱";
            }
        }

        public InputPlayerNameEvent(int endEventID) : base(endEventID)
        {
        }

        public override void Response(Dictionary<byte, object> parameters)
        {
            string name = (string)parameters[(byte)InputStringResponseParameterCode.String];
            PlayerManager.Instance.Player.Name = name;
        }
    }
}

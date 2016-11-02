using System.Collections.Generic;
using System.Linq;

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

        public override bool Response(List<object> informationProviders)
        {
            if(base.Response(informationProviders))
            {
                if(informationProviders.Any(x => x is Player))
                {
                    Player player = informationProviders.First(x => x is Player) as Player;
                    string name = informationProviders.First(x => x is string) as string;

                    player.Name = name;
                    IsCompleted = true;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

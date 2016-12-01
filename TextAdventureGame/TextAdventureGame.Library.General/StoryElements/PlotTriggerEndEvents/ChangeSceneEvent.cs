using System.Collections.Generic;
using MsgPack.Serialization;
using System.Linq;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public class ChangeSceneEvent : PlotTriggerEvent
    {
        [MessagePackMember(id: 1, Name = "sceneID")]
        private int sceneID;
        public override string EventInformation
        {
            get
            {
                return string.Format("更改場景 場景ID： {0}", sceneID);
            }
        }

        public ChangeSceneEvent(int endEventID, int sceneID) : base(endEventID)
        {
            this.sceneID = sceneID;
        }

        public override bool Execute(List<object> informationProviders)
        {
            if(base.Execute(informationProviders))
            {
                Player player = informationProviders.OfType<Player>().FirstOrDefault();
                if(player != null)
                {
                    return Response(new List<object> { player });
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
        public override bool Response(List<object> informationProviders)
        {
            if(base.Response(informationProviders))
            {
                Player player = informationProviders.OfType<Player>().FirstOrDefault();
                if (player != null)
                {
                    player.LocatedSceneID = sceneID;
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

using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents
{
    public abstract class InputStringEvent : PlotTriggerEvent
    {
        protected InputStringEvent(int endEventID) : base(endEventID)
        {
        }

        public override bool Execute(List<object> informationProviders)
        {
            if(base.Execute(informationProviders))
            {
                InputActionProvider inputActionProvider = informationProviders.OfType<InputActionProvider>().FirstOrDefault();
                if (inputActionProvider != null)
                {
                    inputActionProvider.RequestStringInput(this);
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
        public override bool Response(List<object> informationProviders)
        {
            return base.Response(informationProviders) && informationProviders.Any(x => x is string);
        }
    }
}

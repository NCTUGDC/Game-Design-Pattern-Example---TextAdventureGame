using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements
{
    public abstract class PlotTriggerElement
    {
        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 0, Name = "triggerConditions")]
        protected List<PlotTriggerCondition> triggerConditions;

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 1, Name = "triggerEndEvents")]
        protected List<PlotTriggerEvent> triggerEndEvents;

        public IEnumerable<PlotTriggerCondition> TriggerConditions { get { return triggerConditions; } }
        public IEnumerable<PlotTriggerEvent> TriggerEndEvents { get { return triggerEndEvents; } }

        [MessagePackDeserializationConstructor]
        protected PlotTriggerElement(List<PlotTriggerCondition> triggerConditions, List<PlotTriggerEvent> triggerEndEvents)
        {
            this.triggerConditions = triggerConditions;
            this.triggerEndEvents = triggerEndEvents;
        }
        protected PlotTriggerElement()
        {
            triggerConditions = new List<PlotTriggerCondition>();
            triggerEndEvents = new List<PlotTriggerEvent>();
        }

        public void AddCondition(PlotTriggerCondition condition)
        {
            triggerConditions.Add(condition);
        }
        public int RemoveCondition(int conditionID)
        {
            return triggerConditions.RemoveAll(x => x.ConditionID == conditionID);
        }
        public bool IsSufficientPlotTriggerConditions(List<object> informationProviders)
        {
            return informationProviders != null && (informationProviders.Count == 0 || triggerConditions.TrueForAll(x => x.IsEligible(informationProviders)));
        }

        public void AddEndEvent(PlotTriggerEvent endEvent)
        {
            triggerEndEvents.Add(endEvent);
        }
        public int RemoveEndEvent(int endEventID)
        {
            return triggerEndEvents.RemoveAll(x => x.EventID == endEventID);
        }
        public bool ExecuteEvents(List<object> providers)
        {
            return providers != null && (providers.Count == 0 || triggerEndEvents.TrueForAll(x => x.Execute(providers)));
        }
    }
}

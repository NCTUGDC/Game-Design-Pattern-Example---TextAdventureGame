using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements
{
    public abstract class PlotTriggerCondition
    {
        public int ConditionID { get; private set; }
        public abstract string ConditionInformation { get; }

        protected PlotTriggerCondition(int conditionID)
        {
            ConditionID = conditionID;
        }
        public abstract bool IsEligible(List<IPlotTriggerConditionTarget> targets);
    }
}

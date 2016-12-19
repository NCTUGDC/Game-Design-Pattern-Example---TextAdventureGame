using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.StoryElements
{
    public abstract class PlotTriggerCondition
    {
        [MessagePackMember(id: 0, Name = "ConditionID")]
        public int ConditionID { get; private set; }
        public abstract string ConditionInformation { get; }

        public PlotTriggerCondition() { }
        protected PlotTriggerCondition(int conditionID)
        {
            ConditionID = conditionID;
        }
        public abstract bool IsEligible();
    }
}

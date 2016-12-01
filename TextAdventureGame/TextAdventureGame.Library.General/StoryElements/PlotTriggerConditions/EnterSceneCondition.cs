using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.Library.General.StoryElements.PlotTriggerConditions
{
    public class EnterSceneCondition : PlotTriggerCondition
    {
        [MessagePackMember(id: 1, Name = "SceneID")]
        public int SceneID { get; private set; }

        public override string ConditionInformation
        {
            get
            {
                return string.Format("EnterSceneCondition SceneID: {0}", SceneID);
            }
        }

        public EnterSceneCondition(int conditionID, int sceneID) : base(conditionID)
        {
            SceneID = sceneID;
        }

        public override bool IsEligible(List<object> informationProviders)
        {
            if(base.IsEligible(informationProviders))
            {
                return informationProviders.OfType<Scene>().All(x => x.SceneID == SceneID);
            }
            else
            {
                return false;
            }
        }
    }
}

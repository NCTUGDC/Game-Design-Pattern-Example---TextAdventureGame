using MsgPack.Serialization;

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

        public override bool IsEligible()
        {
            return PlayerManager.Instance.Player.LocatedSceneID == SceneID;
        }
    }
}

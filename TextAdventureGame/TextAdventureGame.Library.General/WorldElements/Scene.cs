using MsgPack.Serialization;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.WorldElements
{
    public class Scene
    {
        [MessagePackMember(id: 0, Name = "SceneID")]
        public int SceneID { get; private set; }
        [MessagePackMember(id: 1, Name = "SceneName")]
        public string SceneName { get; private set; }
        [MessagePackMember(id: 2, Name = "npcIDs")]
        private List<int> npcIDs;
        [MessagePackMember(id: 3, Name = "accessibleSceneIDs")]
        private List<int> accessibleSceneIDs;
        public IEnumerable<int> NPC_IDs { get { return npcIDs; } }
        public IEnumerable<int> AccessibleSceneIDs { get { return accessibleSceneIDs; } }
        public int NPC_Count { get { return npcIDs.Count; } }
        public int AccessibleSceneCount { get { return accessibleSceneIDs.Count; } }
        public MonsterZone MonsterZone { get; private set; }

        [MessagePackDeserializationConstructor]
        public Scene() { }
        public Scene(int sceneID, string sceneName)
        {
            SceneID = sceneID;
            SceneName = sceneName;
            npcIDs = new List<int>();
            accessibleSceneIDs = new List<int>();
        }
        public bool ContainsNPC(int npcID)
        {
            return npcIDs.Contains(npcID);
        }
        public bool ContainsAccessibleScene(int sceneID)
        {
            return accessibleSceneIDs.Contains(sceneID);
        }
        public void AddNPC_ID(int npcID)
        {
            if (!ContainsNPC(npcID))
            {
                npcIDs.Add(npcID);
            }
        }
        public bool RemoveNPC(int npcID)
        {
            return npcIDs.Remove(npcID);
        }
        public void AddAccessibleSceneID(int sceneID)
        {
            if (!ContainsAccessibleScene(sceneID))
            {
                accessibleSceneIDs.Add(sceneID);
            }
        }
        public bool RemoveAccessibleSceneID(int sceneID)
        {
            return accessibleSceneIDs.Remove(sceneID);
        }
        public void SetMonsterZone(MonsterZone monsterZone)
        {
            MonsterZone = monsterZone;
        }
    }
}

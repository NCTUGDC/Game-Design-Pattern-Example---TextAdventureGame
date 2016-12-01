using System.Collections.Generic;
using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.WorldElements
{
    public class Scene
    {
        [MessagePackMember(id: 0, Name = "SceneID")]
        public int SceneID { get; private set; }
        [MessagePackMember(id: 1, Name = "SceneName")]
        public string SceneName { get; private set; }
        [MessagePackMember(id: 2, Name = "NPC_Dictionary")]
        private Dictionary<int, NPC> NPC_Dictionary;
        public IEnumerable<NPC> NPCs { get { return NPC_Dictionary.Values; } }
        public int NPC_Count { get { return NPC_Dictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public Scene() { }
        public Scene(int sceneID, string sceneName)
        {
            SceneID = sceneID;
            SceneName = sceneName;
            NPC_Dictionary = new Dictionary<int, NPC>();
        }
        public bool ContainsNPC(int NPC_ID)
        {
            return NPC_Dictionary.ContainsKey(NPC_ID);
        }
        public NPC FindNPC(int NPC_ID)
        {
            if (ContainsNPC(NPC_ID))
            {
                return NPC_Dictionary[NPC_ID];
            }
            else
            {
                return null;
            }
        }
        public void AddNPC(NPC NPC)
        {
            if (!ContainsNPC(NPC.NPC_ID))
            {
                NPC_Dictionary.Add(NPC.NPC_ID, NPC);
            }
        }
        public bool RemoveNPC(int NPC_ID)
        {
            return NPC_Dictionary.Remove(NPC_ID);
        }
    }
}

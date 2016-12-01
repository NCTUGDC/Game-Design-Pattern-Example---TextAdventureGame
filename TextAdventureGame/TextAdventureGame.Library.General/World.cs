using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.Library.General
{
    public class World
    {
        private static World instance;
        public static World Instance { get { return instance; } }

        public static void Initial(World world)
        {
            instance = world;
        }

        public static World LoadWorld(string fileName)
        {
            if (File.Exists(fileName))
            {
                return SerializationHelper.Deserialize<World>(File.ReadAllBytes(fileName));
            }
            else
            {
                return null;
            }
        }
        public static void SaveWorld(string fileName, World world)
        {
            File.WriteAllBytes(fileName, SerializationHelper.Serialize(world));
        }

        [MessagePackMember(id: 0, Name = "WorldName")]
        public string WorldName { get; private set; }
        [MessagePackMember(id: 1, Name = "sceneDictionary")]
        private Dictionary<int, Scene> sceneDictionary;
        public IEnumerable<Scene> Scenes { get { return sceneDictionary.Values; } }
        public int SceneCount { get { return sceneDictionary.Count; } }

        [MessagePackDeserializationConstructor]
        public World() { }
        public World(string worldName)
        {
            WorldName = worldName;
            sceneDictionary = new Dictionary<int, Scene>();
        }
        public bool ContainsScene(int sceneID)
        {
            return sceneDictionary.ContainsKey(sceneID);
        }
        public Scene FindScene(int sceneID)
        {
            if (ContainsScene(sceneID))
            {
                return sceneDictionary[sceneID];
            }
            else
            {
                return null;
            }
        }
        public void AddScene(Scene scene)
        {
            if (!ContainsScene(scene.SceneID))
            {
                sceneDictionary.Add(scene.SceneID, scene);
            }
        }
        public bool RemoveScene(int sceneID)
        {
            return sceneDictionary.Remove(sceneID);
        }
    }
}

using TextAdventureGame.Library.General;
using UnityEngine;

namespace TextAdventureGame.Unity.Scripts.SystemScripts
{
    public class SystemController : MonoBehaviour
    {
        void Awake()
        {
            NPC_Factory.InitialFactory(NPC_Factory.LoadFactory("NPC_Factory"));
            SkillFactory.InitialFactory(SkillFactory.LoadFactory("SkillFactory"));
            MonsterFactory.InitialFactory(MonsterFactory.LoadFactory("MonsterFactory"));
            ItemFactory.InitialFactory(ItemFactory.LoadFactory("ItemFactory"));
            StoreFactory.InitialFactory(StoreFactory.LoadFactory("StoreFactory"));
            World.Initial(World.LoadWorld("World"));
            StoryManager.InitialManager(Story.LoadStory("MainStory"));
            InputManager.InitialManager(new UnityInputManager());
            PlayerManager.InitialManager(new Player());

            PlayerManager.Instance.Player.Inventory.AddItem(ItemFactory.Instance.FindItem(6), 100);
        }
    }
}

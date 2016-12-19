using System.Collections.Generic;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.Library.General
{
    public class StoryManager
    {
        private static StoryManager instance;
        public static StoryManager Instance { get { return instance; } }

        public static void InitialManager(Story story)
        {
            instance = new StoryManager(story);
        }


        public Story Story { get; private set; }
        public StoryManager(Story story)
        {
            Story = story;
        }
    }
}

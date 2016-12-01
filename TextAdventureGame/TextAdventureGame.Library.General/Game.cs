using TextAdventureGame.Library.General.StoryElements;
using System;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public class Game
    {
        public Story MainStory { get; private set; }

        public Game()
        {
            MainStory = Story.LoadStory("MainStory");
        }
    }
}

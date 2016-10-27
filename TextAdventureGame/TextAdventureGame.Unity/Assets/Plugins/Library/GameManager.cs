using TextAdventureGame.Library.General;

namespace TextAdventureGame.Unity.Library
{
    public static class GameManager
    {
        public static Game Game { get; private set; }
        static GameManager()
        {
            Game = new Game();
        }
    }
}

namespace TextAdventureGame.Library.General
{
    public class PlayerManager
    {
        private static PlayerManager instance;
        public static PlayerManager Instance { get { return instance; } }

        public static void InitialManager(Player player)
        {
            instance = new PlayerManager(player);
        }
        public Player Player { get; private set; }
        public PlayerManager(Player player)
        {
            Player = player;
        }
    }
}

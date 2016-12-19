namespace TextAdventureGame.Library.General
{
    public static class LevelEXPTable
    {
        public static int GetLevelUpEXP(int level)
        {
            return level * 100;
        }
    }
}

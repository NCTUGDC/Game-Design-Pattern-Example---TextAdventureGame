using MsgPack.Serialization;

namespace TextAdventureGame.Library.General
{
    public class Monster
    {
        [MessagePackMember(id: 0, Name = "MonsterID")]
        public int MonsterID { get; private set; }
        [MessagePackMember(id: 1, Name = "MonsterName")]
        public string MonsterName { get; private set; }
        [MessagePackMember(id: 2, Name = "AbilityFactors")]
        public AbilityFactors AbilityFactors { get; private set; }
        [MessagePackMember(id: 3, Name = "EXP")]
        public int EXP { get; private set; }

        public Monster() { }
        public Monster(int monsterID, string monsterName, AbilityFactors abilityFactors, int exp)
        {
            MonsterID = monsterID;
            MonsterName = monsterName;
            AbilityFactors = abilityFactors;
            EXP = exp;
        }
    }
}

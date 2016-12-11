using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.WorldElements
{
    public class MonsterZone
    {
        [MessagePackMember(id: 0, Name = "monsterTeams")]
        private List<List<int>> monsterTeams;
        [MessagePackMember(id: 1, Name = "encounterProbability")]
        private List<int> encounterProbability;

        public MonsterZone()
        {
            monsterTeams = new List<List<int>>();
            encounterProbability = new List<int>();
        }
        public List<int> GetMonsterTeam()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            int number = randomGenerator.Next(1, 101);
            for (int i = 0; i < monsterTeams.Count; i++)
            {
                List<int> monsterTeam = monsterTeams[i];
                if (encounterProbability[i] <= number)
                {
                    return monsterTeam;
                }
            }
            return null;
        }
    }
}

using MsgPack.Serialization;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TextAdventureGame.Library.General.WorldElements
{
    public class MonsterZone
    {
        [MessagePackMember(id: 0, Name = "monsterTeams")]
        private List<List<int>> monsterTeams;
        [MessagePackMember(id: 1, Name = "encounterProbability")]
        private List<int> encounterProbability;
        public int EncounterProbability { get { return encounterProbability.FirstOrDefault(); } }

        public MonsterZone() { }
        public MonsterZone(List<List<int>> monsterTeams, List<int> encounterProbability)
        {
            this.monsterTeams = monsterTeams;
            this.encounterProbability = encounterProbability;
        }
        public List<int> GetMonsterTeam()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            int number = randomGenerator.Next(1, 101);
            for (int i = 0; i < monsterTeams.Count; i++)
            {
                if (encounterProbability[i] >= number)
                {
                    return monsterTeams[i];
                }
            }
            return null;
        }
    }
}

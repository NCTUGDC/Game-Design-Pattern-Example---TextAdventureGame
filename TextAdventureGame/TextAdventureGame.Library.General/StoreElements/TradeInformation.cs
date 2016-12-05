using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.StoreElements
{
    public class TradeInformation
    {
        [MessagePackMember(id: 0, Name = "costs")]
        private List<TradeItemInformation> costs;
        [MessagePackIgnore]
        public IEnumerable<TradeItemInformation> Costs { get { return costs.ToList(); } }

        [MessagePackMember(id: 1, Name = "rewards")]
        private List<TradeItemInformation> rewards;
        [MessagePackIgnore]
        public IEnumerable<TradeItemInformation> Rewards { get { return rewards.ToList(); } }

        public void AddCost(TradeItemInformation cost)
        {
            costs.Add(cost);
        }
        public void AddReward(TradeItemInformation reward)
        {
            rewards.Add(reward);
        }
    }
}

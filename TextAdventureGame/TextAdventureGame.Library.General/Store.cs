using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.StoreElements;

namespace TextAdventureGame.Library.General
{
    public class Store
    {
        [MessagePackMember(id: 0, Name = "StoreID")]
        public int StoreID { get; private set; }
        [MessagePackMember(id: 1, Name = "StoreName")]
        public string StoreName { get; private set; }
        [MessagePackMember(id: 2, Name = "tradeInformations")]
        private List<TradeInformation> tradeInformations;
        [MessagePackIgnore]
        public List<TradeInformation> TradeInformations { get { return tradeInformations.ToList(); } }

        [MessagePackDeserializationConstructor]
        public Store() { }
        public Store(int storeID, string storeName)
        {
            StoreID = storeID;
            StoreName = storeName;
            tradeInformations = new List<TradeInformation>();
        }
        public void AddTradeInformation(TradeInformation tradeInformation)
        {
            tradeInformations.Add(tradeInformation);
        }
        public void RemoveTradeInformationAt(int index)
        {
            tradeInformations.RemoveAt(index);
        }
    }
}

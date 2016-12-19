using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.NPCs
{
    public class Seller : NPC
    {
        [MessagePackMember(id: 3, Name = "StoreID")]
        public int StoreID { get; private set; }

        [MessagePackDeserializationConstructor]
        public Seller() { }
        public Seller(int NPC_ID, string name, string conversationContent, int storeID) : base(NPC_ID, name, conversationContent)
        {
            StoreID = storeID;
        }
    }
}

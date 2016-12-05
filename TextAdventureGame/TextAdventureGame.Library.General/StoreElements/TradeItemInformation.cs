using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.StoreElements
{
    public struct TradeItemInformation
    {
        [MessagePackMember(id: 0, Name = "itemID")]
        public int itemID;
        [MessagePackMember(id: 1, Name = "count")]
        public int count;
    }
}

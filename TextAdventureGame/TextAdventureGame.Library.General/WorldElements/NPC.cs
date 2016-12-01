using MsgPack.Serialization;

namespace TextAdventureGame.Library.General.WorldElements
{
    public class NPC
    {
        [MessagePackMember(id: 0, Name = "NPC_ID")]
        public int NPC_ID { get; private set; }
        [MessagePackMember(id: 1, Name = "Name")]
        public string Name { get; private set; }
        [MessagePackMember(id: 2, Name = "ConversationContent")]
        public string ConversationContent { get; set; }

        [MessagePackDeserializationConstructor]
        public NPC() { }
        public NPC(int NPC_ID, string name, string conversationContent)
        {
            this.NPC_ID = NPC_ID;
            Name = name;
            ConversationContent = conversationContent;
        }
    }
}

using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;
using TextAdventureGame.Library.General.Effectors;

namespace TextAdventureGame.Library.General.ItemElements
{
    public class Consumable : Item
    {
        [MessagePackMember(id: 2, Name = "ConsumableID")]
        public int ConsumableID { get; protected set; }

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 3, Name = "effectors")]
        protected List<ConsumableEffector> effectors;
        public IEnumerable<ConsumableEffector> Effectors { get { return effectors; } }

        [MessagePackDeserializationConstructor]
        public Consumable() { }
        public Consumable(int itemID, string itemName, int consumableID) : base(itemID, itemName)
        {
            ConsumableID = consumableID;
            effectors = new List<ConsumableEffector>();
        }
        public bool Use(AbilityFactors abilityFactors)
        {
            if (effectors.Count != 0)
            {
                return effectors.All(x => x.Affect(abilityFactors));
            }
            else
            {
                return false;
            }
        }
        public void AddEffector(ConsumableEffector effector)
        {
            effectors.Add(effector);
        }
        public void RemoveEffector(ConsumableEffector effector)
        {
            effectors.Remove(effector);
        }
    }
}

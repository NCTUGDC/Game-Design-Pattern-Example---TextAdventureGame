using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.ItemElements
{
    public class Consumable : ItemComponent
    {
        public int ConsumableID { get; protected set; }

        public override ItemComponentTypeCode ItemComponentTypeCode { get { return ItemComponentTypeCode.Consumables; } }
        protected List<Effector> effectors;
        public IEnumerable<Effector> Effectors { get { return effectors; } }

        public Consumable(int consumableID)
        {
            ConsumableID = consumableID;
            effectors = new List<Effector>();
        }
        public override bool Use(List<object> targets)
        {
            if (effectors.Count != 0)
            {
                return effectors.All(x => x.Affect(targets));
            }
            else
            {
                return false;
            }
        }
        public void AddEffector(Effector effector)
        {
            effectors.Add(effector);
        }
        public void RemoveEffector(Effector effector)
        {
            effectors.Remove(effector);
        }
    }
}

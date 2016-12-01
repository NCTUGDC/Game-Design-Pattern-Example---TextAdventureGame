using System.Collections.Generic;

namespace TextAdventureGame.Library.General.ItemElements
{
    public abstract class ItemComponent
    {
        public abstract ItemComponentTypeCode ItemComponentTypeCode { get; }
        public abstract bool Use(List<object> targets);
    }
}

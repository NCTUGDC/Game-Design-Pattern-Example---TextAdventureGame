using System.Collections.Generic;

namespace TextAdventureGame.Library.General
{
    public abstract class Effector
    {
        public abstract bool Affect(List<object> targets);
    }
}

using System.Collections.Generic;

namespace TextAdventureGame.Library.General
{
    public interface IInputActionCallbackTarget
    {
        void Response(Dictionary<byte, object> parameters);
    }
}

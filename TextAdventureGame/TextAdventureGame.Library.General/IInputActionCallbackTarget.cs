using System.Collections.Generic;

namespace TextAdventureGame.Library.General
{
    public interface IInputActionCallbackTarget
    {
        bool Response(List<object> informationProviders);
    }
}

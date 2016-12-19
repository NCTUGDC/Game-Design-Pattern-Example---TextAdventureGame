using System.Collections.Generic;

namespace TextAdventureGame.Library.General
{
    public abstract class InputManager
    {
        public static InputManager Instance { get; private set; }

        public static void InitialManager(InputManager inputManager)
        {
            Instance = inputManager;
        }

        private IInputActionCallbackTarget callbackTarget;
        public int TalkingNPC_ID { get; set; }

        public virtual void InputStringRequest(IInputActionCallbackTarget callbackTarget)
        {
            this.callbackTarget = callbackTarget;
        }
        public virtual void InputStringResponse(Dictionary<byte, object> parameters)
        {
            callbackTarget.Response(parameters);
        }
    }
}

using System.Collections.Generic;

namespace TextAdventureGame.Library.General
{
    public class InputManager
    {
        private static InputManager instance;
        public static InputManager Instance { get { return instance; } }

        public static void InitialManager()
        {
            instance = new InputManager();
        }

        private IInputActionCallbackTarget callbackTarget;
        public int TalkingNPC_ID { get; set; }

        public void InputStringRequest(IInputActionCallbackTarget callbackTarget)
        {
            this.callbackTarget = callbackTarget;
        }
        public void InputStringResponse(Dictionary<byte, object> parameters)
        {
            callbackTarget.Response(parameters);
        }
    }
}

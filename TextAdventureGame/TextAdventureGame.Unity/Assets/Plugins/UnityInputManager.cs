using System;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents.InputStringEvents;

namespace TextAdventureGame.Unity
{
    public class UnityInputManager : InputManager
    {
        private event Action onPlayerNameInputRequest;
        public event Action OnPlayerNameInputRequest { add { onPlayerNameInputRequest += value; } remove { onPlayerNameInputRequest -= value; } }

        public override void InputStringRequest(IInputActionCallbackTarget callbackTarget)
        {
            base.InputStringRequest(callbackTarget);
            if(callbackTarget is InputPlayerNameEvent)
            {
                if(onPlayerNameInputRequest != null)
                    onPlayerNameInputRequest.Invoke();
                PlayerManager.Instance.Player.LocatedSceneID = 1;
            }
        }
    }
}

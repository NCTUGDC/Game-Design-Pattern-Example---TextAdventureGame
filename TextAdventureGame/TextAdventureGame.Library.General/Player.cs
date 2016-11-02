using System;

namespace TextAdventureGame.Library.General
{
    public class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                onNameChange?.Invoke(name);
            }
        }

        private event Action<string> onNameChange;
        public event Action<string> OnNameChange { add { onNameChange += value; } remove { onNameChange -= value; } }
    }
}

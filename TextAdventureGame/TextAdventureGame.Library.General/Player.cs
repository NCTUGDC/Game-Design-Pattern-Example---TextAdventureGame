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

        private int locatedSceneID;
        public int LocatedSceneID
        {
            get { return locatedSceneID; }
            set
            {
                locatedSceneID = value;
                onLocatedSceneIDChange?.Invoke(locatedSceneID);
            }
        }
        public Inventory Inventory { get; private set; }

        private event Action<string> onNameChange;
        public event Action<string> OnNameChange { add { onNameChange += value; } remove { onNameChange -= value; } }

        private event Action<int> onLocatedSceneIDChange;
        public event Action<int> OnLocatedSceneIDChange { add { onLocatedSceneIDChange += value; } remove { onLocatedSceneIDChange -= value; } }

        public Player()
        {
            Inventory = new Inventory();
        }
    }
}

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
        public AbilityFactors AbilityFactors { get; private set; }

        private int levelUpEXP;
        public int LevelUpEXP
        {
            get { return levelUpEXP; }
            set
            {
                levelUpEXP = value;
                exp = 0;
                onLevelUpEXPChange?.Invoke(levelUpEXP);
            }
        }

        private int exp;
        public int EXP
        {
            get { return exp; }
            set
            {
                int totalEXP = value;
                while(totalEXP > LevelUpEXP)
                {
                    totalEXP -= LevelUpEXP;
                    onEXP_Change?.Invoke(LevelUpEXP);
                }
                exp = Math.Max(totalEXP, 0);
                onEXP_Change?.Invoke(exp);
            }
        }

        private int abilityPoint;
        public int AbilityPoint
        {
            get { return abilityPoint; }
            set
            {
                abilityPoint = Math.Max(value, 0);
                onAbilityPointChange?.Invoke(abilityPoint);
            }
        }

        private event Action<string> onNameChange;
        public event Action<string> OnNameChange { add { onNameChange += value; } remove { onNameChange -= value; } }

        private event Action<int> onLocatedSceneIDChange;
        public event Action<int> OnLocatedSceneIDChange { add { onLocatedSceneIDChange += value; } remove { onLocatedSceneIDChange -= value; } }

        private event Action<int> onLevelUpEXPChange;
        public event Action<int> OnLevelUpEXPChange { add { onLevelUpEXPChange += value; } remove { onLevelUpEXPChange -= value; } }

        private event Action<int> onEXP_Change;
        public event Action<int> OnEXP_Change { add { onEXP_Change += value; } remove { onEXP_Change -= value; } }

        private event Action<int> onAbilityPointChange;
        public event Action<int> OnAbilityPointChange { add { onAbilityPointChange += value; } remove { onAbilityPointChange -= value; } }

        public Player()
        {
            Inventory = new Inventory();
            AbilityFactors = new AbilityFactors
            {
                Level = 1,
                MaxHP = 50,
                HP = 50,
                MaxSP = 25,
                SP = 25,
                Power = 1,
                Magic = 1,
                Speed = 1,
                Sensibility = 1
            };
            LevelUpEXP = 100;
            EXP = 0;
            AbilityPoint = 6;
        }
    }
}

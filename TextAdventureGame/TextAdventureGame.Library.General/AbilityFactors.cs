using System;

namespace TextAdventureGame.Library.General
{
    public class AbilityFactors
    {
        #region properties
        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                int delta = value - level;
                level = value;
                onLevelChange?.Invoke(this, delta);
            }
        }

        private int maxHP;
        public int MaxHP
        {
            get { return maxHP; }
            set
            {
                maxHP = value;
                onMaxHPChange?.Invoke(maxHP);
            }
        }

        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = Math.Max(Math.Min(MaxHP, value), 0);
                onHPChange?.Invoke(hp);
            }
        }

        private int maxSP;
        public int MaxSP
        {
            get { return maxSP; }
            set
            {
                maxSP = value;
                onMaxSPChange?.Invoke(maxSP);
            }
        }

        private int sp;
        public int SP
        {
            get { return sp; }
            set
            {
                sp = Math.Max(Math.Min(MaxSP, value), 0);
                onSPChange?.Invoke(sp);
            }
        }

        private int power;
        public int Power
        {
            get { return power; }
            set
            {
                power = value;
                onPowerChange?.Invoke(power);
            }
        }

        private int magic;
        public int Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                onMagicChange?.Invoke(magic);
            }
        }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                onSpeedChange?.Invoke(speed);
            }
        }

        private int sensibility;
        public int Sensibility
        {
            get { return sensibility; }
            set
            {
                sensibility = value;
                onSensibilityChange?.Invoke(sensibility);
            }
        }

        #endregion

        #region events
        private event Action<AbilityFactors, int> onLevelChange;
        public event Action<AbilityFactors, int> OnLevelChange { add { onLevelChange += value; } remove { onLevelChange -= value; } }

        private event Action<int> onMaxHPChange;
        public event Action<int> OnMaxHPChange { add { onMaxHPChange += value; } remove { onMaxHPChange -= value; } }

        private event Action<int> onHPChange;
        public event Action<int> OnHPChange { add { onHPChange += value; } remove { onHPChange -= value; } }

        private event Action<int> onMaxSPChange;
        public event Action<int> OnMaxSPChange { add { onMaxSPChange += value; } remove { onMaxSPChange -= value; } }

        private event Action<int> onSPChange;
        public event Action<int> OnSPChange { add { onSPChange += value; } remove { onSPChange -= value; } }

        private event Action<int> onPowerChange;
        public event Action<int> OnPowerChange { add { onPowerChange += value; } remove { onPowerChange -= value; } }

        private event Action<int> onMagicChange;
        public event Action<int> OnMagicChange { add { onMagicChange += value; } remove { onMagicChange -= value; } }

        private event Action<int> onSpeedChange;
        public event Action<int> OnSpeedChange { add { onSpeedChange += value; } remove { onSpeedChange -= value; } }

        private event Action<int> onSensibilityChange;
        public event Action<int> OnSensibilityChange { add { onSensibilityChange += value; } remove { onSensibilityChange -= value; } }
        #endregion
    }
}

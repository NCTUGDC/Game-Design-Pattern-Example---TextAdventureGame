using MsgPack.Serialization;
using System;

namespace TextAdventureGame.Library.General
{
    public class AbilityFactors
    {
        #region properties
        [MessagePackMember(id: 0, Name = "level")]
        private int level;
        [MessagePackIgnore]
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

        [MessagePackMember(id: 1, Name = "maxHP")]
        private int maxHP;
        [MessagePackIgnore]
        public int MaxHP
        {
            get { return maxHP; }
            set
            {
                maxHP = value;
                onMaxHPChange?.Invoke(maxHP);
            }
        }

        [MessagePackMember(id: 2, Name = "hp")]
        private int hp;
        [MessagePackIgnore]
        public int HP
        {
            get { return hp; }
            set
            {
                hp = Math.Max(Math.Min(MaxHP, value), 0);
                onHPChange?.Invoke(hp);
            }
        }

        [MessagePackMember(id: 3, Name = "maxSP")]
        private int maxSP;
        [MessagePackIgnore]
        public int MaxSP
        {
            get { return maxSP; }
            set
            {
                maxSP = value;
                onMaxSPChange?.Invoke(maxSP);
            }
        }

        [MessagePackMember(id: 4, Name = "sp")]
        private int sp;
        [MessagePackIgnore]
        public int SP
        {
            get { return sp; }
            set
            {
                sp = Math.Max(Math.Min(MaxSP, value), 0);
                onSPChange?.Invoke(sp);
            }
        }

        [MessagePackMember(id: 5, Name = "power")]
        private int power;
        [MessagePackIgnore]
        public int Power
        {
            get { return power; }
            set
            {
                power = value;
                onPowerChange?.Invoke(power);
            }
        }

        [MessagePackMember(id: 6, Name = "magic")]
        private int magic;
        [MessagePackIgnore]
        public int Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                onMagicChange?.Invoke(magic);
            }
        }

        [MessagePackMember(id: 7, Name = "agile")]
        private int agile;
        [MessagePackIgnore]
        public int Agile
        {
            get { return agile; }
            set
            {
                agile = value;
                onAgileChange?.Invoke(agile);
            }
        }

        [MessagePackMember(id: 8, Name = "sensibility")]
        private int sensibility;
        [MessagePackIgnore]
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

        private event Action<int> onAgileChange;
        public event Action<int> OnAgileChange { add { onAgileChange += value; } remove { onAgileChange -= value; } }

        private event Action<int> onSensibilityChange;
        public event Action<int> OnSensibilityChange { add { onSensibilityChange += value; } remove { onSensibilityChange -= value; } }
        #endregion
    }
}

namespace TextAdventureGame.Library.General
{
    public class BattleFactors
    {
        public static BattleFactors FromAbilityFactors(AbilityFactors abilityFactors)
        {
            return new BattleFactors
            {
                maxHealthPoint = abilityFactors.MaxHP,
                healthPoint = abilityFactors.HP,
                maxSkillPoint = abilityFactors.MaxSP,
                skillPoint = abilityFactors.SP,
                physicalAttackPoint = abilityFactors.Power * 2,
                physicalDefencePoint = abilityFactors.Power + abilityFactors.Level,
                magicalAttackPoint = abilityFactors.Magic * 2,
                magicalDefencePoint = abilityFactors.Magic + abilityFactors.Level,
                accuracyPoint = abilityFactors.Agile * 2 + abilityFactors.Sensibility,
                evasionPoint = abilityFactors.Agile + abilityFactors.Sensibility * 2,
                speedPoint = abilityFactors.Agile * 3
            };
        }

        public int maxHealthPoint;
        public int maxSkillPoint;
        public int healthPoint;
        public int skillPoint;
        public int physicalAttackPoint;
        public int magicalAttackPoint;
        public int physicalDefencePoint;
        public int magicalDefencePoint;
        public int accuracyPoint;
        public int evasionPoint;
        public int speedPoint;
    }
}

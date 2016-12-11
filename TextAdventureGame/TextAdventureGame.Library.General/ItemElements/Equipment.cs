using System.Collections.Generic;
using System.Linq;
using MsgPack.Serialization;
using TextAdventureGame.Library.General.Effectors;

namespace TextAdventureGame.Library.General.ItemElements
{
    public class Equipment : Item
    {
        [MessagePackMember(id: 2, Name = "EquipmentID")]
        public int EquipmentID { get; protected set; }

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 3, Name = "effectors")]
        protected List<EquipmentEffector> equipmentEffectors;
        public IEnumerable<EquipmentEffector> EquipmentEffectors { get { return equipmentEffectors; } }

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 4, Name = "abilityCoditionEffectors")]
        protected List<AbilityConditionEffector> abilityCoditionEffectors;
        public IEnumerable<AbilityConditionEffector> AbilityCoditionEffectors { get { return abilityCoditionEffectors; } }

        [MessagePackMember(id: 5, Name = "EquipmentType")]
        public EquipmentType EquipmentType { get; private set; }

        [MessagePackDeserializationConstructor]
        public Equipment() { }
        public Equipment(int itemID, string itemName, int equipmentID, EquipmentType equipmentType) : base(itemID, itemName)
        {
            EquipmentID = equipmentID;
            equipmentEffectors = new List<EquipmentEffector>();
            abilityCoditionEffectors = new List<AbilityConditionEffector>();
            EquipmentType = equipmentType;
        }
        public bool IsMatchedAbilityCodition(Player player)
        {
            if (abilityCoditionEffectors.Count != 0)
            {
                return abilityCoditionEffectors.All(x => x.IsSufficient(player.AbilityFactors));
            }
            else
            {
                return true;
            }
        }
        public BattleFactors Use(BattleFactors battleFactors)
        {
            foreach(var effector in equipmentEffectors)
            {
                battleFactors = effector.Use(battleFactors);
            }
            return battleFactors;
        }
        public void AddEquipmentEffector(EquipmentEffector equipmentEffector)
        {
            equipmentEffectors.Add(equipmentEffector);
        }
        public void RemoveEquipmentEffector(EquipmentEffector equipmentEffector)
        {
            equipmentEffectors.Remove(equipmentEffector);
        }
        public void AddAbilityConditionEffector(AbilityConditionEffector abilityCoditionEffector)
        {
            abilityCoditionEffectors.Add(abilityCoditionEffector);
        }
        public void RemoveAbilityConditionEffector(AbilityConditionEffector abilityCoditionEffector)
        {
            abilityCoditionEffectors.Remove(abilityCoditionEffector);
        }
    }
}

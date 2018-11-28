using UnityEngine;

public class PlayerStats : CharacterStats {

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("OnItemChanged in PlayerStats");
        if(newItem != null)
        {
            armour += newItem.armourModifier;
            damage += newItem.damageModifier;
        }
        
        if(oldItem != null)
        {
            armour -= oldItem.armourModifier;
            damage -= oldItem.damageModifier;
        }
    }

    public override void Die()
    {
        Debug.Log("Player died");
        deathHandler.PlayerDeath();
    }
}

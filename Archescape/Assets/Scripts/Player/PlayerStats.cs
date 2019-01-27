
public class PlayerStats : CharacterStats {

    public PlayerStats()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 1;
        armour = 0;
        attackSpeed = 1f;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
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
}

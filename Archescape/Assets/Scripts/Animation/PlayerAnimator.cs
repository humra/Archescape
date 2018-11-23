using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator {

    public WeaponAnimations[] weaponAnimations;

    private Dictionary<Equipment, AnimationClip[]> weaponAnimationsDictionary;

    protected override void Start()
    {
        base.Start();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDictionary = new Dictionary<Equipment, AnimationClip[]>();
        foreach(WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDictionary.Add(a.weapon, a.clips);
        }
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null && newItem.equipSlot == EquipmentSlot.weapon)
        {
            animator.SetLayerWeight(1, 1);
            if(weaponAnimationsDictionary.ContainsKey(newItem))
            {
                currentAttackAnimationSet = weaponAnimationsDictionary[newItem];
            }
        }
        else if(newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimationSet = defaultAttackAnimationSet;
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.offhand)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.offhand)
        {
            animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}

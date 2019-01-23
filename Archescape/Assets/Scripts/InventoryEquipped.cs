using UnityEngine;

public class InventoryEquipped : MonoBehaviour {

    #region Singleton
    public static InventoryEquipped instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of equipped inventory found");
            GameObject.Destroy(this);
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemEquipped();
    public OnItemEquipped onItemEquippedCallback;
    public IEquipmentHandler equipmentHandler;
    public InventorySlot[] equipment;

    private EquipmentManager equipmentManager;
    private Item[] equipmentSlots = new Item[5];

    private void Start()
    {
        equipmentManager = EquipmentManager.instance;

        equipment[0] = GameObject.FindGameObjectWithTag(UITagRepository.headEquipment).GetComponent<InventorySlot>();
        equipment[1] = GameObject.FindGameObjectWithTag(UITagRepository.chestEquipment).GetComponent<InventorySlot>();
        equipment[2] = GameObject.FindGameObjectWithTag(UITagRepository.legsEquipment).GetComponent<InventorySlot>();
        equipment[3] = GameObject.FindGameObjectWithTag(UITagRepository.weaponEquipment).GetComponent<InventorySlot>();
        equipment[4] = GameObject.FindGameObjectWithTag(UITagRepository.offHandEquipment).GetComponent<InventorySlot>();
    }

    public void UpdateUI()
    {
        for(int i = 0; i < equipment.Length; i++)
        {
            if (equipmentSlots[i] == null)
            {
                equipment[i].ClearSlot();
            }
            else
            {
                equipment[i].ClearSlot();
                equipment[i].AddItem(equipmentSlots[i]);
            }
        }
    }

    public void EquipItem(Equipment newEquipment)
    {
        if(newEquipment.isDefaultItem)
        {
            return;
        }

        equipmentSlots[(int)newEquipment.equipSlot] = newEquipment;

        UpdateUI();
    }

    public void UnequipItem(Item unequippedItem)
    {
        equipmentHandler.MoveItemToInventory(unequippedItem);
        Equipment unequippedEquipment = unequippedItem as Equipment;

        if(unequippedEquipment == null)
        {
            return;
        }

        equipmentSlots[(int)unequippedEquipment.equipSlot] = null;

        if(unequippedEquipment.equipSlot != EquipmentSlot.offhand && unequippedEquipment.equipSlot != EquipmentSlot.weapon)
        {
            EquipmentManager.instance.EquipDefaultItem(unequippedEquipment.equipSlot);
        }

        UpdateUI();
    }
}

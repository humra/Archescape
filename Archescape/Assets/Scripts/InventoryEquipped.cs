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
    }

    private void UpdateUI()
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

        UpdateUI();
    }
}

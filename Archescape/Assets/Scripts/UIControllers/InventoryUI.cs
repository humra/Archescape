using UnityEngine;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    private GameObject itemsParent;
    [SerializeField]
    private GameObject inventoryUI;

    private Inventory inventory;
    private InventorySlot[] slots;

    private void Start()
    {
        itemsParent = GameObject.FindGameObjectWithTag(UITagRepository.itemsParent);
        inventoryUI = GameObject.FindGameObjectWithTag(UITagRepository.inventoryPanel);

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();

        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(UIKeybindRepository.inventory))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

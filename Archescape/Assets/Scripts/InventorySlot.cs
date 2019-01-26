using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler, IBeginDragHandler {

    private Item item;
    private Vector3 positionBeforeDrag;

    public Image icon;
    public Button removeButton;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = newItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.DropItem(item);
    }

    public void OnRemoveEquippedItemButton()
    {
        InventoryEquipped.instance.UnequipItem(item);
    }

    public void UseItem()
    {
        if(item == null)
        {
            return;
        }

        item.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = positionBeforeDrag;
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventory = GameObject.FindWithTag(UITagRepository.itemsParent).GetComponent<RectTransform>();

        if(!RectTransformUtility.RectangleContainsScreenPoint(inventory, Input.mousePosition))
        {
            Inventory.instance.DropItem(item);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        positionBeforeDrag = transform.position;
    }
}

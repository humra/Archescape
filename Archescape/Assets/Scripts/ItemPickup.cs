using UnityEngine;

public class ItemPickup : Interactible {


    public Item item;

    public override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picked up " + item.itemName);
        if(Inventory.instance.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}

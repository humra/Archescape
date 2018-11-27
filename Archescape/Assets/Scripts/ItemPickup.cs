using UnityEngine;

public class ItemPickup : Interactible {

    public Item item;
    public IItemHandler itemHandler;

    private void OnMouseDown()
    {
        itemHandler.MoveToItemInteractionPointLocation(transform, radius);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(TagRepository.player))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if(Inventory.instance.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}

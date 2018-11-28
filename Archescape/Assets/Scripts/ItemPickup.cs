using UnityEngine;

public class ItemPickup : Interactible {

    public Item item;
    public IItemHandler itemHandler;

    private bool toBePickedUp = false;

    private void OnMouseDown()
    {
        itemHandler.MoveToItemInteractionPointLocation(transform, interactionRadius);
        toBePickedUp = true;

        if(GetComponent<Collider>().bounds.Contains(itemHandler.GetPlayerPosition()))
        {
            PickUp();
            toBePickedUp = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(TagRepository.player) && toBePickedUp)
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

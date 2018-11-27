using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();
    public int inventorySpace = 20;

    public bool AddItem(Item newItem)
    {
        if(!newItem.isDefaultItem && items.Count < inventorySpace)
        {
            items.Add(newItem);
            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }

            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

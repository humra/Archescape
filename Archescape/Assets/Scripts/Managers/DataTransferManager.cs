using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferManager : MonoBehaviour {

    #region Singleton

    public static DataTransferManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of data transfer manager found");
            GameObject.Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public List<Item> inventory;
    public List<Equipment> equipment;

    private void Start()
    {
        inventory = new List<Item>();
        equipment = new List<Equipment>();
    }

    public void AddInventoryItem(Item newItem)
    {
        inventory.Add(newItem);
    }

    public void AddEquipmentItem(Equipment newEquipment)
    {
        equipment.Add(newEquipment);
    }
}

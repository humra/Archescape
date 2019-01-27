using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IItemHandler, IEnemyHandler, IDeathHandler, IInventoryHandler, IUIHandler, IEquipmentHandler, IPortalHandler {

    private Camera mainCam;
    private PlayerController player;
    private CombatManager combatManager;
    private Inventory inventory;
    private HealthBarUI playerHealthBar;
    private InventoryEquipped inventoryEquipped;
    private EquipmentManager equipmentManager;
    private DataTransferManager dataTransferManager;

    [SerializeField]
    private LayerMask walkableMask;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        player.deathHandler = this;

        playerHealthBar = player.GetComponentInChildren<HealthBarUI>();

        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.targetMesh = player.GetComponentInChildren<SkinnedMeshRenderer>();

        InjectInterfaceIntoInteractibles();
        InjectEnemyDependencies();

        GetComponent<SettingsUI>().uiHandler = this;
        GetComponent<PauseMenuUI>().uiHandler = this;
    }

    void Start () {
        mainCam = Camera.main;

        combatManager = GetComponent<CombatManager>();
        combatManager.playerStats = player.stats;
        combatManager.playerHealthBarUI = player.GetComponentInChildren<HealthBarUI>();
        combatManager.DisablePlayerHealthBar();

        inventory = GetComponent<Inventory>();
        inventory.inventoryHandler = this;

        inventoryEquipped = GetComponent<InventoryEquipped>();
        inventoryEquipped.equipmentHandler = this;

        dataTransferManager = GameObject.FindGameObjectWithTag(TagRepository.dataTransferManager).GetComponent<DataTransferManager>();

        ReadDataTransferInventory();
        ReadDataTransferEquipment();
    }
	
	void Update () {

        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (walkableMask == (walkableMask | 1 << hit.collider.gameObject.layer))
                {
                    player.MoveToPoint(hit.point);
                    player.RemoveTarget();
                    DisengagePlayerCombat();
                }
            }
        }
    }

    private void InjectInterfaceIntoInteractibles()
    {
        ItemPickup[] items = FindObjectsOfType<ItemPickup>();

        foreach(ItemPickup item in items)
        {
            item.itemHandler = this;
        }

        Portal[] portals = FindObjectsOfType<Portal>();

        foreach(Portal portal in portals)
        {
            portal.portalHandler = this;
        }
    }

    private void InjectEnemyDependencies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        foreach(EnemyController enemy in enemies)
        {
            enemy.enemyHandler = this;
            enemy.target = player.transform;
        }
    }

    #region ItemInterface

    public void MoveToItemInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance)
    {
        player.MoveToPoint(interactionPointLocation.position, interactionStoppingDistance);
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void MoveItemToInventory(Item newItem)
    {
        Equipment tempEquipment = newItem as Equipment;
        EquipmentManager.instance.Unequip((int)tempEquipment.equipSlot);
    }

    #endregion

    #region Combat

    public void SetPlayerFocus(GameObject enemy)
    {
        player.SetTarget(enemy);
    }

    public void EngageCombat(EnemyController enemy)
    {
        combatManager.enemyStats = enemy.stats;
        combatManager.playerBeingAttacked = true;
        combatManager.enemyHealthBarUI = enemy.GetComponentInChildren<HealthBarUI>();
        combatManager.EnablePlayerHealthBar();
    }

    public void EngagePlayerCombat(EnemyController enemyToEngage)
    {
        combatManager.enemyBeingAttacked = true;
        combatManager.enemyStats = enemyToEngage.stats;
        combatManager.enemyHealthBarUI = enemyToEngage.GetComponentInChildren<HealthBarUI>();
    }

    private void DisengagePlayerCombat()
    {
        combatManager.enemyBeingAttacked = false;
        combatManager.enemyHealthBarUI = null;
    }

    public void MoveToEnemyInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance)
    {
        player.MoveToPoint(interactionPointLocation.position, interactionStoppingDistance);
    }

    #endregion

    #region Death

    public void EnemyDeath(EnemyController enemy)
    {
        GameObject.Destroy(enemy.gameObject);
    }

    public void PlayerDeath()
    {
        combatManager.StopAllCombat();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    public void ItemDropped(Item item)
    {
        GameObject newItem = (GameObject)Instantiate(Resources.Load("Equipment/" + item.itemName), player.transform.position, player.transform.rotation);
        newItem.GetComponent<ItemPickup>().itemHandler = this;
    }

    #region UIHandling

    public void SetEnvironmentalVolume(float newValue)
    {
        AudioManager.instance.SetEnvironmentalAudioVolume(newValue);
    }

    public void SetSoundtrackVolume(float newValue)
    {
        AudioManager.instance.SetSoundtrackAudioVolume(newValue);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Portals

    public void MoveToPortal(Transform portalLocation)
    {
        player.MoveToPoint(portalLocation.position);
    }

    public void StopMoving()
    {
        player.StopMoving();
    }

    public void LoadScene(int sceneIndex)
    {
        FillDataTransferInventory();
        FillDataTransferEquipment();

        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        FillDataTransferInventory();
        FillDataTransferEquipment();

        SceneManager.LoadScene(sceneName);
    }

    #endregion

    #region DataTransfer

    private void FillDataTransferInventory()
    {
        foreach(Item item in inventory.items)
        {
            dataTransferManager.AddInventoryItem(item);
        }
    }

    private void FillDataTransferEquipment()
    {
        foreach(Equipment equipment in equipmentManager.GetCurrentEquipment())
        {
            dataTransferManager.AddEquipmentItem(equipment);
        }
    }

    private void ReadDataTransferInventory()
    {
        if(dataTransferManager.inventory.Count == 0)
        {
            Debug.Log("Data Transfer Manager empty inventory!");
            return;
        }

        foreach(Item item in dataTransferManager.inventory)
        {
            Inventory.instance.AddItem(item);
        }
    }

    private void ReadDataTransferEquipment()
    {
        if(dataTransferManager.equipment.Count == 0)
        {
            Debug.Log("Data Transfer Manager empty equipment!");
            return;
        }

        foreach(Equipment equipment in dataTransferManager.equipment)
        {
            if(equipment == null)
            {
                //Weapon and offhand can be null here, so we skip that case
                continue;
            }

            if(!equipment.isDefaultItem)
            {
                equipment.Use();
            }
        }
    }

    #endregion
}

#region Interfaces

public interface IItemHandler
{
    void MoveToItemInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance);
    Vector3 GetPlayerPosition();
}

public interface IEnemyHandler
{
    void SetPlayerFocus(GameObject enemy);
    void EngageCombat(EnemyController enemy);
    void EngagePlayerCombat(EnemyController enemyToEngage);
    void MoveToEnemyInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance);
    void EnemyDeath(EnemyController enemy);
}

public interface IDeathHandler
{
    void PlayerDeath();
}

public interface IInventoryHandler
{
    void ItemDropped(Item item);
}

public interface IUIHandler
{
    void SetEnvironmentalVolume(float newValue);
    void SetSoundtrackVolume(float newValue);
    void QuitGame();
}

public interface IEquipmentHandler
{
    void MoveItemToInventory(Item newItem);
}

public interface IPortalHandler
{
    void MoveToPortal(Transform portalLocation);
    void StopMoving();
    void LoadScene(int sceneIndex);
    void LoadScene(string sceneName);
}

#endregion
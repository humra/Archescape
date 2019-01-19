using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IItemHandler, IEnemyHandler, IDeathHandler, IInventoryHandler, IUIHandler, IEquipmentHandler {

    private Camera mainCam;
    private PlayerController player;
    private CombatManager combatManager;
    private Inventory inventory;
    private HealthBarUI playerHealthBar;
    private InventoryEquipped inventoryEquipped;
    private EquipmentManager equipmentManager;

    [SerializeField]
    private LayerMask walkableMask;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        player.GetComponent<PlayerStats>().deathHandler = this;

        playerHealthBar = player.GetComponentInChildren<HealthBarUI>();

        InjectInterfaceIntoInteractibles();
        InjectEnemyDependencies();
    }

    void Start () {
        mainCam = Camera.main;

        combatManager = GetComponent<CombatManager>();
        combatManager.playerStats = player.GetComponent<PlayerStats>();
        combatManager.playerHealthBarUI = player.GetComponentInChildren<HealthBarUI>();
        combatManager.DisablePlayerHealthBar();

        inventory = GetComponent<Inventory>();
        inventory.inventoryHandler = this;

        inventoryEquipped = GetComponent<InventoryEquipped>();
        inventoryEquipped.equipmentHandler = this;

        equipmentManager = GetComponent<EquipmentManager>();

        GetComponent<SettingsUI>().uiHandler = this;
        GetComponent<PauseMenuUI>().uiHandler = this;
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
    }

    private void InjectEnemyDependencies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            enemy.enemyHandler = this;
            enemy.target = player.transform;
            enemy.GetComponent<CharacterStats>().deathHandler = this;
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

    public void EngageCombat(CharacterStats enemyStats)
    {
        combatManager.enemyStats = enemyStats;
        combatManager.playerBeingAttacked = true;
        combatManager.EnablePlayerHealthBar();
    }

    public void EngagePlayerCombat(CharacterStats enemyStats)
    {
        combatManager.enemyBeingAttacked = true;
        combatManager.enemyStats = enemyStats;
        combatManager.enemyHealthBarUI = combatManager.enemyStats.GetComponentInChildren<HealthBarUI>();
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

    public void EnemyDeath(GameObject deadEnemy)
    {
        combatManager.StopAllCombat();
        DestroyImmediate(deadEnemy);
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
    void EngageCombat(CharacterStats enemyStats);
    void EngagePlayerCombat(CharacterStats enemyStats);
    void MoveToEnemyInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance);
}

public interface IDeathHandler
{
    void EnemyDeath(GameObject deadEnemy);
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

#endregion
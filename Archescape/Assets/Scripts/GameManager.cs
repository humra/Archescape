using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IItemHandler, IEnemyHandler, IDeathHandler {

    private Camera mainCam;
    private PlayerController player;
    private CombatManager combatManager;

    [SerializeField]
    private LayerMask walkableMask;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        player.GetComponent<PlayerStats>().deathHandler = this;
        InjectInterfaceIntoInteractibles();
        InjectEnemyDependencies();
    }

    void Start () {
        mainCam = Camera.main;
        combatManager = GetComponent<CombatManager>();
        combatManager.playerStats = player.GetComponent<PlayerStats>();
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

    public void MoveToItemInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance)
    {
        player.MoveToPoint(interactionPointLocation.position, interactionStoppingDistance);
    }

    public void SetPlayerFocus(GameObject enemy)
    {
        player.SetTarget(enemy);
    }

    public void EngageCombat(CharacterStats enemyStats)
    {
        combatManager.enemyStats = enemyStats;
        combatManager.playerBeingAttacked = true;
    }

    public void EngagePlayerCombat()
    {
        combatManager.enemyBeingAttacked = true;
    }

    private void DisengagePlayerCombat()
    {
        combatManager.enemyBeingAttacked = false;
    }

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
}

public interface IItemHandler
{
    void MoveToItemInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance);
}

public interface IEnemyHandler
{
    void SetPlayerFocus(GameObject enemy);
    void EngageCombat(CharacterStats enemyStats);
    void EngagePlayerCombat();
}

public interface IDeathHandler
{
    void EnemyDeath(GameObject deadEnemy);
    void PlayerDeath();
}

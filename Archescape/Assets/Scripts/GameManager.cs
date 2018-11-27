using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, IItemHandler, IEnemyHandler {

    private Camera mainCam;
    private PlayerController player;

    [SerializeField]
    private LayerMask walkableMask;
    [SerializeField]
    private LayerMask interactibleMask;

    private void Awake()
    {
        InjectInterfaceIntoInteractibles();
        InjectInterfaceIntoEnemies();
    }

    void Start () {
        mainCam = Camera.main;
        player = FindObjectOfType<PlayerController>();
	}
	
	void Update () {

        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
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

    private void InjectInterfaceIntoEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            enemy.enemyHandler = this;
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
}

public interface IItemHandler
{
    void MoveToItemInteractionPointLocation(Transform interactionPointLocation, float interactionStoppingDistance);
}

public interface IEnemyHandler
{
    void SetPlayerFocus(GameObject enemy);
}

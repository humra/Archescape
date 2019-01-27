using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Interactible {

    public IEnemyHandler enemyHandler;
    public float lookRadius = 10f;
    public Transform target;

    [SerializeField]
    private EnemyType enemyType;

    private NavMeshAgent agent;
    public CharacterStats stats;
    private bool toBeAttacked = false;

    private void Start()
    {
        stats = new CharacterStats(enemyType);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(stats.currentHealth <= 0)
        {
            enemyHandler.EnemyDeath(this);
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                enemyHandler.EngageCombat(this);
                FaceTarget();
            }
        }

        if(distance <= interactionRadius && toBeAttacked)
        {
            enemyHandler.EngagePlayerCombat(this);
        }
    }

    private void OnMouseDown()
    {
        enemyHandler.SetPlayerFocus(this.gameObject);
        toBeAttacked = true;
        enemyHandler.MoveToEnemyInteractionPointLocation(interactionTransform, interactionRadius);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

public enum EnemyType
{
    Skeleton
};

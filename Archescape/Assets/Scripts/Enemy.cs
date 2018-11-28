using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactible {

    public IEnemyHandler enemyHandler;
    public float lookRadius = 10f;
    public Transform target;

    private NavMeshAgent agent;
    private CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                enemyHandler.EngageCombat(myStats);
                FaceTarget();
            }
        }
    }

    private void OnMouseDown()
    {
        enemyHandler.SetPlayerFocus(this.gameObject);
        enemyHandler.EngagePlayerCombat();
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

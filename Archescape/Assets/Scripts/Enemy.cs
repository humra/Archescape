using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactible {

    public IEnemyHandler enemyHandler;

    public float lookRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    //private CharacterCombat combat;

    //private PlayerManager playerManager;
    private CharacterStats myStats;

    private void Start()
    {
        //playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();

        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        //combat = GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //CharacterStats targetStats = target.GetComponent<CharacterStats>();
                //if(targetStats != null)
                //{
                //    combat.Attack(targetStats);
                //}
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

    public override void Interact()
    {
        base.Interact();
        //CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        //if(playerCombat != null)
        //{
        //    playerCombat.Attack(myStats);
        //}
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

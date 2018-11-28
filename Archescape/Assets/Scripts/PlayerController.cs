using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject target;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if(target != null)
        {
            FaceTarget();

            if(target.GetComponent<Enemy>() != null)
            {
                agent.stoppingDistance = target.GetComponent<Enemy>().interactionRadius * 0.9f;
                agent.SetDestination(target.transform.position);
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.stoppingDistance = 0f;
        agent.SetDestination(point);
    }

    public void MoveToPoint(Vector3 point, float stoppingDistance)
    {
        agent.stoppingDistance = stoppingDistance * 0.9f;
        agent.SetDestination(point);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void RemoveTarget()
    {
        target = null;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

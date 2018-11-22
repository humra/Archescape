using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour {

    NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		
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
}

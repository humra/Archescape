using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour {

    Camera cam;
    public LayerMask mask;
    NavMeshAgent agent;

	void Start () {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, mask))
            {
                MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Check for interactible
            }
        }
    }

    private void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}

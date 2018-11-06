using UnityEngine;

public class GameManager : MonoBehaviour {

    Camera mainCam;
    PlayerController player;

    [SerializeField]
    private LayerMask walkableMask;
    [SerializeField]
    private LayerMask interactibleMask;

    void Start () {
        mainCam = Camera.main;
        player = FindObjectOfType<PlayerController>();
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Action(hit);
            }
        }
    }

    private void Action(RaycastHit hit)
    {
        if(walkableMask == (walkableMask | 1 << hit.collider.gameObject.layer))
        {
            Debug.Log("Move");
            player.MoveToPoint(hit.point);
        }
        else if(interactibleMask == (interactibleMask | 1 << hit.collider.gameObject.layer))
        {
            Debug.Log("Interact");
            Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();
            player.MoveToPoint(interactible.interactionTransform.position, interactible.radius);
            interactible.Interact();
        }
    }
}

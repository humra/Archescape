using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    private Camera mainCam;
    private PlayerController player;

    [SerializeField]
    private LayerMask walkableMask;
    [SerializeField]
    private LayerMask interactibleMask;

    void Start () {
        mainCam = Camera.main;
        player = FindObjectOfType<PlayerController>();
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

            if(Physics.Raycast(ray, out hit))
            {
                Action(hit);
            }
        }
    }

    private void Action(RaycastHit hit)
    {
        player.RemoveTarget();

        if(walkableMask == (walkableMask | 1 << hit.collider.gameObject.layer))
        {
            player.MoveToPoint(hit.point);
        }
        else if(interactibleMask == (interactibleMask | 1 << hit.collider.gameObject.layer))
        {
            Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();
            player.MoveToPoint(interactible.interactionTransform.position, interactible.radius);
            player.SetTarget(interactible.gameObject);
            interactible.Interact();
        }
    }
}

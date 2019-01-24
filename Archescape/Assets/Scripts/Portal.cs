using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactible {

    public IPortalHandler portalHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(TagRepository.player))
        {
            portalHandler.StopMoving();
            portalHandler.LoadScene(1);
        }
    }

    private void OnMouseDown()
    {
        portalHandler.MoveToPortal(transform);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactible {

    public IPortalHandler portalHandler;
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(TagRepository.player))
        {
            portalHandler.StopMoving();
            portalHandler.LoadScene(sceneToLoad);
        }
    }

    private void OnMouseDown()
    {
        portalHandler.MoveToPortal(transform);
    }
}

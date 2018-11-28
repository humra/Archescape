using UnityEngine;

public class Interactible : MonoBehaviour {

    public float interactionRadius = 1f;
    public Transform interactionTransform;

    public virtual void Interact() { }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = this.transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
    }
}

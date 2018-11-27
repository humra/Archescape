using UnityEngine;

public class Interactible : MonoBehaviour {

    public float radius = 3f;
    public Transform interactionTransform;

    public virtual void Interact() { }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = this.transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}

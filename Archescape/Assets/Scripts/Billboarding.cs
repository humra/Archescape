using UnityEngine;

public class Billboarding : MonoBehaviour {

    public Camera mainCamera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}

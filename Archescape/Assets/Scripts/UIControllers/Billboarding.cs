using UnityEngine;

public class Billboarding : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}

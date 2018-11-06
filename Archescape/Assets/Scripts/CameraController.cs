using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform target;
    private float currentZoom = 10f;
    private float currentYaw = 0f;

    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        target = GameObject.FindWithTag(TagRepository.player).transform;
        RenderSettings.fog = false;
    }

    void Update () {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * CameraControlsConfiguration.zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, CameraControlsConfiguration.minZoom, CameraControlsConfiguration.maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * CameraControlsConfiguration.yawSpeed * Time.deltaTime;
	}

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * CameraControlsConfiguration.pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}

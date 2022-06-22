using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float movementVelocityFactor;
    [SerializeField] private float zoomVelocity;
    [SerializeField] private float maxZoomSize;
    [SerializeField] private float minZoomSize;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    float GetMovementVelocity()
    {
        return Time.deltaTime * movementVelocityFactor * camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * GetMovementVelocity());
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * GetMovementVelocity());
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.down * GetMovementVelocity());
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * GetMovementVelocity());
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y < 0)
            camera.orthographicSize = Mathf.Min(camera.orthographicSize * zoomVelocity, maxZoomSize);
        if (Input.mouseScrollDelta.y > 0)
            camera.orthographicSize = Mathf.Max(camera.orthographicSize / zoomVelocity, minZoomSize);
    }
}

using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float movementVelocityFactor;
    [SerializeField] private float zoomVelocity;
    [SerializeField] private float maxZoomSize;
    [SerializeField] private float minZoomSize;
    private Camera target;

    private void Start()
    {
        target = GetComponent<Camera>();
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
            transform.Translate(Vector3.up * GetMovementVelocity());
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  
            transform.Translate(Vector3.left * GetMovementVelocity());
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
            transform.Translate(Vector3.down * GetMovementVelocity());
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
            transform.Translate(Vector3.right * GetMovementVelocity());
    }

    private float GetMovementVelocity()
    {
        return Time.deltaTime * movementVelocityFactor * target.orthographicSize;
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y < 0) ZoomOut();
        if (Input.mouseScrollDelta.y > 0) ZoomIn();
    }

    private void ZoomIn()
    {
        target.orthographicSize = Mathf.Max(target.orthographicSize / zoomVelocity, minZoomSize);
    }

    private void ZoomOut()
    {
        target.orthographicSize = Mathf.Min(target.orthographicSize * zoomVelocity, maxZoomSize);
    }
}

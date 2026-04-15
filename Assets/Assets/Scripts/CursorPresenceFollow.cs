using UnityEngine;

public class CursorPresenceFollow : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zDepth = 10f;
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 velocity;

    void Reset()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;

        Vector3 mouse = Input.mousePosition;
        mouse.z = zDepth;

        Vector3 target = cam.ScreenToWorldPoint(mouse);
        target.z = 0f;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            target,
            ref velocity,
            smoothTime
        );
    }
}
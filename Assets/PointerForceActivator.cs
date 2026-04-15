using UnityEngine;

public class PointerForceActivator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private Transform cursorPresence;
    [SerializeField] private Camera cam;

    [Header("Force Settings")]
    [SerializeField] private float activeForceMultiplier = 0.35f;
    [SerializeField] private float inactiveForceMultiplier = 0f;
    [SerializeField] private float positionSmoothTime = 0.08f;

    private Vector3 velocity;

    void Reset()
    {
        cam = Camera.main;
    }

    void Update()
    {
        bool isPressing = false;
        Vector3 screenPos = Input.mousePosition;

        // Mobile / touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began ||
                touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                isPressing = true;
                screenPos = touch.position;
            }
        }
        // Mouse
        else if (Input.GetMouseButton(0))
        {
            isPressing = true;
            screenPos = Input.mousePosition;
        }

        // Keep the presence object under the active pointer
        if (cursorPresence != null && cam != null)
        {
            Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            world.z = 0f;

            cursorPresence.position = Vector3.SmoothDamp(
                cursorPresence.position,
                world,
                ref velocity,
                positionSmoothTime
            );
        }

        float targetMultiplier = isPressing ? activeForceMultiplier : inactiveForceMultiplier;

        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps == null) continue;

            var externalForces = ps.externalForces;
            externalForces.enabled = true;
            externalForces.multiplier = targetMultiplier;
        }
    }
}
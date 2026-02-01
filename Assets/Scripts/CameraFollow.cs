using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    private Rigidbody2D targetRb;

    [Header("Follow Settings")]
    public float followSmoothSpeed = 5f;

    [Header("Look Ahead Settings")]
    public float lookAheadDistance = 3f;
    public float lookAheadSmoothSpeed = 5f;
    public float movementThreshold = 0.1f;

    private float currentLookAheadX;
    private float targetLookAheadX;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (target == null || targetRb == null) return;

        float moveX = targetRb.linearVelocity.x;

        // Decide look-ahead direction
        if (Mathf.Abs(moveX) > movementThreshold)
            targetLookAheadX = Mathf.Sign(moveX) * lookAheadDistance;
        else
            targetLookAheadX = 0f;

        // Smooth look-ahead transition
        currentLookAheadX = Mathf.Lerp(
            currentLookAheadX,
            targetLookAheadX,
            lookAheadSmoothSpeed * Time.deltaTime
        );

        // Final camera position
        Vector3 targetPosition = new Vector3(
            target.position.x + currentLookAheadX,
            target.position.y,
            transform.position.z
        );

        // Smooth follow
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            1f / followSmoothSpeed
        );
    }
}

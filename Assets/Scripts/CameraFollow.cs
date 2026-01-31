using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float followSpeed = 5f;

    [Header("Look Ahead")]
    public float lookAheadDistance = 3f;
    public float lookAheadSpeed = 5f;

    private float currentLookAheadX;
    private float targetLookAheadX;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        if (target == null) return;

        float moveDir = Mathf.Sign(target.GetComponent<Rigidbody2D>().linearVelocity.x);

        // If player is moving, shift camera
        if (Mathf.Abs(target.GetComponent<Rigidbody2D>().linearVelocity.x) > 0.1f)
        {
            targetLookAheadX = moveDir * lookAheadDistance;
        }
        else
        {
            targetLookAheadX = 0f;
        }

        // Smoothly interpolate look-ahead
        currentLookAheadX = Mathf.Lerp(
            currentLookAheadX,
            targetLookAheadX,
            lookAheadSpeed * Time.deltaTime
        );

        Vector3 targetPosition = new Vector3(
            target.position.x + currentLookAheadX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            1f / followSpeed
        );
    }
}
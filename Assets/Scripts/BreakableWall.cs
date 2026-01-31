using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [Header("Break Settings")]
    public float breakForceThreshold = 15f;
    public float decayRate = 6f;

    private float accumulatedForce = 0f;
    private bool isBroken = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBroken) return;

        // Only player
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        // Only Stone mask
        if (MaskController.Instance.currentMask != MaskTypes.Stone)
            return;

        Rigidbody2D playerRb = collision.rigidbody;
        if (playerRb == null) return;

        // ✅ Use impact momentum (reliable)
        float impactForce =
            collision.relativeVelocity.magnitude * playerRb.mass;

        accumulatedForce += impactForce;

        if (accumulatedForce >= breakForceThreshold)
        {
            Break();
        }
    }

    private void FixedUpdate()
    {
        // Decay in physics time
        accumulatedForce = Mathf.Max(
            0f,
            accumulatedForce - decayRate * Time.fixedDeltaTime
        );
    }

    private void Break()
    {
        isBroken = true;
        Debug.Log("Breakable wall destroyed (STONE + FORCE)");
        Destroy(gameObject);
    }
}

using UnityEngine;

public class MagnetAttractor : MonoBehaviour
{
    [Header("Magnet Hold Settings")]
    public float attractionRadius = 5f;
    public float holdOffsetX = 1.5f;
    public float holdSpeed = 3f;

    private void FixedUpdate()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attractionRadius
        );

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("IronBox"))
                continue;

            Rigidbody2D ironRb = hit.attachedRigidbody;
            if (ironRb == null)
                continue;

            if (MaskController.Instance.currentMask == MaskTypes.Magnet)
            {
                HoldIron(ironRb);
            }
            else
            {
                FreezeIron(ironRb);
            }
        }
    }

    private void HoldIron(Rigidbody2D ironRb)
    {
        float targetX = transform.position.x + holdOffsetX;

        float newX = Mathf.MoveTowards(
            ironRb.position.x,
            targetX,
            holdSpeed * Time.fixedDeltaTime
        );

        // Move ONLY on X axis
        ironRb.MovePosition(new Vector2(newX, ironRb.position.y));

        // Kill residual X velocity
        ironRb.linearVelocity = new Vector2(0f, ironRb.linearVelocity.y);
    }

    private void FreezeIron(Rigidbody2D ironRb)
    {
        // Completely stop movement when not magnet
        ironRb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position + Vector3.right * holdOffsetX + Vector3.up * 0.5f,
            transform.position + Vector3.right * holdOffsetX + Vector3.down * 0.5f
        );
    }
}
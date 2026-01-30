using UnityEngine;

public class FallableWall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0.7f;   // tuned for logs
        rb.angularDamping = 3f;      // prevents wild spinning
    }

    public void Fall()
    {
        if (hasFallen) return;    // 🔒 only fall once

        hasFallen = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}

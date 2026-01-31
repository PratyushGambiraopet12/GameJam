using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fallable":
                HandleFallableWall(collision.gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Die();
        }
    }

    private void HandleFallableWall(GameObject wall)
    {
        if (MaskController.Instance.currentMask == MaskTypes.Stone)
        {
            wall.GetComponent<FallableWall>()?.Fall();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // respawn / reload logic
    }
}

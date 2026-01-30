using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Breakable":
                collision.gameObject.GetComponent<BreakableWall>()?.Break();
                break;

            case "Fallable":
                HandleFallableWall(collision.gameObject);
                break;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Die();
        }
    }


    private void HandleFallableWall(GameObject wall)
    {
        if (MaskController.Instance.currentMask == MaskTypes.Stone)
        {
            Debug.Log("Stone mask activated: Triggering fallable wall.");
            wall.GetComponent<FallableWall>()?.Fall();
        }
    }



    public void Die()
    {
        Debug.Log("Player has died.");
        // Additional death handling logic here
    }
}

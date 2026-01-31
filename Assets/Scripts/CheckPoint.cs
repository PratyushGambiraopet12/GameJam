using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        if (activated) return;

        activated = true;
        CheckPointManager.Instance.SetCheckpoint(transform.position);

        Debug.Log("Checkpoint activated at " + transform.position);

        // Optional: change visual (color, animation, etc.)
    }
}

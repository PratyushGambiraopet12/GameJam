using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        activated = true;

        CheckpointManager.Instance.SetCheckpoint(
            transform.position,
            EnergySystem.Instance.currentEnergy
        );
    }
}
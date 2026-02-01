using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player == null || activated) return;

        activated = true;

        CheckPointManager.Instance.SetCheckpoint(transform.position, EnergySystem.Instance.currentEnergy);
        Debug.Log("Checkpoint activated at position and energy is saved: " + EnergySystem.Instance.currentEnergy);
    }

}


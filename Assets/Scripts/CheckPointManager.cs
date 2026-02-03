using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Vector3 lastCheckpointPosition;
    private float savedEnergy;
    private bool hasCheckpoint = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    public void SetCheckpoint(Vector3 position, float energy)
    {
        lastCheckpointPosition = position;
        savedEnergy = energy;
        hasCheckpoint = true;

        Debug.Log($"Checkpoint saved at {position}, Energy: {energy}");
    }

    
    public Vector3 GetLastCheckpointPosition(Vector3 fallback)
    {
        return hasCheckpoint ? lastCheckpointPosition : fallback;
    }

    public float GetSavedEnergy(float fallback)
    {
        return hasCheckpoint ? savedEnergy : fallback;
    }

    
    public void ResetCheckpoints()
    {
        hasCheckpoint = false;
        Debug.Log("Checkpoints reset");
    }
}
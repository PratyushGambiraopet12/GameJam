using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance { get; private set; }

    private float SavedEnergy;  
    private bool hasCheckpoint = false;
    private Vector3 LastCheckpoint;

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
        LastCheckpoint = position;
        SavedEnergy = energy;
        hasCheckpoint = true;

    }

    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }   

    public Vector3 GetLastCheckpointPosition(Vector3 Fallback)
    {
        return hasCheckpoint ? LastCheckpoint : Fallback;   
    }


    public float GetSavedEnergy(float DefaultEnergy)
    {
        return hasCheckpoint ? SavedEnergy : DefaultEnergy;
    }

    public void ResetCheckpoint()
    {
        hasCheckpoint = false;
        
    }
}

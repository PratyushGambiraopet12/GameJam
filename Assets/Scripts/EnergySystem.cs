using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance { get; private set; }

    [Header("EnergySystem")]

    public float MaxEnergy = 100f;  
    public float currentEnergy;

    [Header("Energy Consumption Rates")]
    public float deafaultRate = 5f;
    public float StoneDrain = 15f;
    public float MagnetDrain = 7f;
    public float FeatherDrain = 8f;

    private bool isDepleted = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentEnergy = MaxEnergy;
    }

    private void Update()
    {
        if(isDepleted)
            return;

        
    }


   
}

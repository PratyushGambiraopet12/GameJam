using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance { get; private set; }

    [Header("EnergySystem")]

    public float MaxEnergy = 100f;  
    public float currentEnergy;

    [Header("Energy Consumption Rates")]
    public float deafaultRate = 0.5f;
    public float StoneDrain = 3f;
    public float MagnetDrain = 1.5f;
    public float FeatherDrain = 1f;


    public float energyTimeScale = 0.25f;

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

        float Drain = GetDrainForCurrentmask();

        if (Drain <= 0f) return;

        currentEnergy -= Drain * Time.deltaTime * energyTimeScale;    

        if(currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            isDepleted = true;
            HandleDrain();
        }

    }

    private float GetDrainForCurrentmask()
    {
        switch(MaskController.Instance.currentMask)
        {
            case MaskTypes.Default:
                return deafaultRate;
            case MaskTypes.Stone:
                return StoneDrain;
            case MaskTypes.Magnet:
                return MagnetDrain;
            case MaskTypes.Feather:
                return FeatherDrain;
            default:
                return deafaultRate;
        }
    }

    private void HandleDrain()
    {
        Debug.Log("Handling Energy Drain");

        FindAnyObjectByType<PlayerController>().PLayerDie();
    }
   
}

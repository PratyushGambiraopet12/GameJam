using System.Collections;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance { get; private set; }

    [Header("Energy")]
    public float MaxEnergy = 100f;
    public float currentEnergy;

    [Header("Drain Per Second")]
    public float defaultDrain = 2.5f;
    public float stoneDrain = 10f;
    public float magnetDrain = 3.5f;
    public float featherDrain = 4f;

    [Header("Timing")]
    public float energyTimeScale = 0.25f; 
    public float energyDeathDelay = 1.5f;

    private bool isDepleted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentEnergy = MaxEnergy;
    }

    private void Update()
    {
        if (isDepleted)
            return;

        float drain = GetDrainForCurrentMask();
        if (drain <= 0f)
            return;

        
        if (!IsPlayerMoving())
            return;

        currentEnergy -= drain * Time.deltaTime * energyTimeScale;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, MaxEnergy);

        if (currentEnergy <= 0f)
        {
            StartCoroutine(EnergyDeathRoutine());
        }
    }

    private float GetDrainForCurrentMask()
    {
        switch (MaskController.Instance.currentMask)
        {
            case MaskTypes.Default: return defaultDrain;
            case MaskTypes.Stone: return stoneDrain;
            case MaskTypes.Magnet: return magnetDrain;
            case MaskTypes.Feather: return featherDrain;
            default: return 0f;
        }
    }


    private IEnumerator EnergyDeathRoutine()
    {
        isDepleted = true;

        PlayerController.Instance.DisableControl();

        yield return new WaitForSeconds(energyDeathDelay);

        // Full restart behavior
        CheckpointManager.Instance.ResetCheckpoints();
        currentEnergy = MaxEnergy;

        PlayerController.Instance.RespawnAtStart();
        PlayerController.Instance.EnableControl();

        isDepleted = false;
    }

    private bool IsPlayerMoving()
    {
        Rigidbody2D rb = PlayerController.Instance.GetComponent<Rigidbody2D>();

        // Small threshold to ignore physics noise
        return Mathf.Abs(rb.linearVelocity.x) > 0.05f;
    }
}
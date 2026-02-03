using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    [Header("References")]
    public Slider energySlider;

    [Header("Smoothness")]
    public float smoothSpeed = 8f;

    private void Start()
    {
        if (EnergySystem.Instance == null)
            return;

        energySlider.minValue = 0f;
        energySlider.maxValue = EnergySystem.Instance.MaxEnergy;
        energySlider.value = EnergySystem.Instance.currentEnergy;
    }

    private void Update()
    {
        if (EnergySystem.Instance == null)
            return;

        float targetValue = EnergySystem.Instance.currentEnergy;

        energySlider.value = Mathf.Lerp(
            energySlider.value,
            targetValue,
            Time.deltaTime * smoothSpeed
        );
    }
}
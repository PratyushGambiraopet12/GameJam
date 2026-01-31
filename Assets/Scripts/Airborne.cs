using JetBrains.Annotations;
using UnityEngine;

public class Airborne : MonoBehaviour
{
    public float BasicFlyMechanics = 12f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        PlayerController player = collision.GetComponent<PlayerController>();

        if (rb == null || player == null) return;

        float liftMultiplier = GetLiftMultiplier();

        if (liftMultiplier <= 0f)
            return;

        rb.AddForce(
            Vector2.up * BasicFlyMechanics * liftMultiplier,
            ForceMode2D.Force
        );
    }




    private float GetLiftMultiplier()
    {
        switch(MaskController.Instance.currentMask)
        {
            case MaskTypes.Feather: return 1.0f;   // strongest
            case MaskTypes.Default: return 0.8f;
            case MaskTypes.Magnet: return 0.5f;
            case MaskTypes.Stone: return 0f;     // no lift
            default: return 0f;


        }
    }
}

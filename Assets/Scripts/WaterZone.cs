using UnityEngine;

public class WaterZone : MonoBehaviour
{
    public float BuoyancyForce = 15f;
    public float MaxSinkSpeed = -4f;



    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        PlayerController player = collision.GetComponent<PlayerController>();   


        if(rb == null || player == null)
        {
            return;
        }

        float BouoyancyMultiplier = GetBuoyancyMultiplier();

        if (BouoyancyMultiplier > 0f)
        {
            rb.AddForce(
                Vector2.up * BuoyancyForce * BouoyancyMultiplier,
                ForceMode2D.Force
            );
        }



        if (rb.linearVelocity.y < MaxSinkSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, MaxSinkSpeed);
        }

    }


    private float GetBuoyancyMultiplier()
    {
        switch (MaskController.Instance.currentMask)
        {
            case MaskTypes.Feather:
                return 3f;   // floats strongly

            case MaskTypes.Default:
                return 0.4f;   // slow sink

            case MaskTypes.Magnet:
                return 0.2f;   // faster sink

            case MaskTypes.Stone:
                return 0f;     // no buoyancy (drowns)

            default:
                return 0f;
        }
    }
}

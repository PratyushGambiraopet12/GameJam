using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;




public class SpikeManager : MonoBehaviour
{
    public SpikeType Spikes;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController Player = collision.GetComponent<PlayerController>();
        if (Player == null) return;


        if (ShouldKillPlayer())
        {
            Player.PLayerDie();
        }
        
    }


    private bool ShouldKillPlayer()
    {
        MaskTypes mask = MaskController.Instance.currentMask;

        switch (Spikes)
        {
            case SpikeType.Metal:
                // Universal death
                return true;

            case SpikeType.Wooden:
                // Only Default & Feather die
                return mask == MaskTypes.Default || mask == MaskTypes.Feather;

            default:
                return false;
        }
    }
}

using UnityEngine;
using UnityEngine.Rendering;

public class MaskController : MonoBehaviour
{
    public MaskTypes currentMask;
    
    private Rigidbody2D Rb;
    private SpriteRenderer SR;



    [Header("Mask Properties")]
    public float SwitchCost= 1.0f;
    
    public static MaskController Instance { get; private set; }

    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SR = GetComponent<SpriteRenderer>();    

    }


    public void SwitchMask(MaskTypes newMask)
    {
        if (currentMask == newMask) return;


        currentMask  = newMask;
        ApplyMask(newMask);
    }


    public void ApplyMask(MaskTypes Mask)
    {
        Rb.mass = 3f;
        Rb.gravityScale = 2.5f;

        switch (Mask)
        {

            case MaskTypes.Default:
                Rb.mass = 3f;
                Rb.gravityScale = 2.5f;
                SR.color = Color.white;
                PlayerController.Instance.MoveSpeed = 4.5f;   
                Debug.Log("Default Mask Applied");
                break;

            case MaskTypes.Stone:
                Rb.mass = 6f;
                Rb.gravityScale = 4f;
                SR.color = Color.gray;
                PlayerController.Instance.MoveSpeed = 2f;
                Debug.Log("Stone Mask Applied");
                break;

            case MaskTypes.Feather:
                Rb.mass = 0.5f;
                Rb.gravityScale = 1.5f;
                SR.color = Color.blue;
                PlayerController.Instance.MoveSpeed = 6f;
                Debug.Log("Feather Mask Applied");  
                break;

            case MaskTypes.Magnet:
                Rb.mass = 4f;
                Rb.gravityScale = 3f;
                SR.color = Color.cyan;
                PlayerController.Instance.MoveSpeed = 3f;
                Debug.Log("Magnet Mask Applied");
                break;
        }

    }
}

using UnityEngine;

public class MaskController : MonoBehaviour
{
    public MaskTypes currentMask;

    private Rigidbody2D Rb;
    private SpriteRenderer SR;

    [Header("Mask Sprites")]
    public Sprite defaultSprite;
    public Sprite stoneSprite;
    public Sprite featherSprite;
    public Sprite magnetSprite;

    [Header("Mask Properties")]
    public float SwitchCost = 1.0f;

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

        currentMask = newMask;
        ApplyMask(newMask);
    }

    public void ApplyMask(MaskTypes mask)
    {
        // Reset defaults before applying specific mask
        Rb.mass = 3f;
        Rb.gravityScale = 2.5f;
        PlayerController.Instance.moveSpeed = 4.5f;

        switch (mask)
        {
            case MaskTypes.Default:
                SR.sprite = defaultSprite;
                Debug.Log("Default Mask Applied");
                break;

            case MaskTypes.Stone:
                Rb.mass = 6f;
                Rb.gravityScale = 3.5f;
                PlayerController.Instance.moveSpeed = 2f;
                SR.sprite = stoneSprite;
                Debug.Log("Stone Mask Applied");
                break;

            case MaskTypes.Feather:
                Rb.mass = 0.5f;
                Rb.gravityScale = 1.5f;
                PlayerController.Instance.moveSpeed = 6f;
                SR.sprite = featherSprite;
                Debug.Log("Feather Mask Applied");
                break;

            case MaskTypes.Magnet:
                Rb.mass = 4f;
                Rb.gravityScale = 3f;
                PlayerController.Instance.moveSpeed = 3f;
                SR.sprite = magnetSprite;
                Debug.Log("Magnet Mask Applied");
                break;
        }
    }
}

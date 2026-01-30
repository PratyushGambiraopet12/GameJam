using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;


public enum MaskTypes
{  
    Default,
    Stone,
    Magnet,
    Feather

}
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }   

    [Header("Movement Settings")]   
    public float MoveSpeed;
    public float JumpForce;
    public float acceleration = 12f;
    public float deceleration = 16f;


    [Header("physics")]
    private Rigidbody2D myRb;
    private float InputX;
    private bool isGrounded;
    public LayerMask GroundLayer;
    private float GroundCheckDistance = 1f;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }


    private void Update()
    {
        
        HandleMovement();

        HandleJump();

        if (UserInput.Instance.JumpPressedThisFrame())
        {
            Jump();
            UserInput.Instance.JumpInput = false;
        }

        var input = UserInput.Instance.Controls.Movement;


        if (input.MaskDefault.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Default);

        if (input.MaskStone.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Stone);

        if (input.MaskFeather.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Feather);

        if (input.MaskMagnet.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Magnet);
    }

    

    public void HandleMovement()
    {
        InputX = UserInput.Instance.MoveInput.x;
        float targetSpeed = InputX * MoveSpeed;
        float accel = Mathf.Abs(InputX) > 0.01f ? acceleration : deceleration;

        float newX = Mathf.MoveTowards(
            myRb.linearVelocity.x,
            targetSpeed,
            accel * Time.deltaTime
        );
        myRb.linearVelocity = new Vector2(newX, myRb.linearVelocity.y);
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayer);
    }


    private void Jump()
    {
        if (isGrounded)
        {
            myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, JumpForce);
        }
    }
}


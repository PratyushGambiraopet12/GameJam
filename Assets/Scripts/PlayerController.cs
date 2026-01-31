using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;


public enum MaskTypes
{  
    Default,
    Stone,
    Magnet,
    Feather

}

public enum SpikeType
{
    Wooden,
    Metal
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Vector3 StartPoint;

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
        StartPoint = transform.position;

    }


    private void Update()
    {


        HandleInput();
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

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();


    }


    public void HandleInput()
    {
        InputX = UserInput.Instance.MoveInput.x;

    }

    public void HandleMovement()
    {
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



    public void PLayerDie()
    {
       Debug.Log("Player Died");
       Respawn();

    }

    private void Respawn()
    {
        myRb.linearVelocity = Vector2.zero;

        Vector3 respawnPos =
            CheckPointManager.Instance.GetRespawnPosition(StartPoint);

        transform.position = respawnPos;
    }



}


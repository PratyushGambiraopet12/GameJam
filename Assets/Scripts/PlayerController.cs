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

    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public float acceleration = 12f;
    public float deceleration = 16f;
    

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;

    private Rigidbody2D rb;
    private float inputX;
    private bool isGrounded;
    private bool canControl = true;

    private Vector3 startPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
       
    }

    private void Update()
    {
        if (!canControl || TutorialManager.Instance.IsTutorialOpen)
            return;

        if (UserInput.Instance.JumpPressedThisFrame())
            Jump();

        var input = UserInput.Instance.Controls.Movement;

        if (input.MaskDefault.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Default);

        if (input.MaskStone.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Stone);

        if (input.MaskMagnet.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Magnet);

        if (input.MaskFeather.WasPressedThisFrame())
            MaskController.Instance.SwitchMask(MaskTypes.Feather);
    }


    private void FixedUpdate()
    {
        if (!canControl || TutorialManager.Instance.IsTutorialOpen)
        {
            rb.linearVelocity = Vector2.zero; // freeze player
            return;
        }

        HandleMovement();
        CheckGround();
    }


    private void HandleMovement()
    {
        
        inputX = UserInput.Instance.MoveInput.x;
        
        float targetSpeed = inputX * moveSpeed;
        float accel = Mathf.Abs(inputX) > 0.01f ? acceleration : deceleration;

        float newX = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetSpeed,
            accel * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );
    }

    private void Jump()
    {
        if (!isGrounded)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

  
    public void PlayerDie()
    {
        RespawnAtCheckpoint();
    }

    private void RespawnAtCheckpoint()
    {
        rb.linearVelocity = Vector2.zero;

        Vector3 spawnPos =
            CheckpointManager.Instance.GetLastCheckpointPosition(startPosition);

        float savedEnergy =
            CheckpointManager.Instance.GetSavedEnergy(EnergySystem.Instance.MaxEnergy);

        transform.position = spawnPos;
        EnergySystem.Instance.currentEnergy = savedEnergy;
    }

    
    public void RespawnAtStart()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        MaskController.Instance.SwitchMask(MaskTypes.Default);
    }

   

    public void DisableControl()
    {
        canControl = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void EnableControl()
    {
        canControl = true;
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public float MoveSpeed;
    public float JumpForce;
    public float acceleration = 12f;
    public float deceleration = 16f;

    [Header("Physics")]
    private Rigidbody2D myRb;
    private float inputX;
    private bool isGrounded;
    public LayerMask GroundLayer;
    private float groundCheckDistance = 1f;

    [Header("Position")]
    private Vector3 startPosition;

    private bool canControl = true;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        startPosition = transform.position;
    }

    private void Update()
    {

        if (!canControl)
            return;
        inputX = UserInput.Instance.MoveInput.x;

        if (UserInput.Instance.JumpPressedThisFrame())
            Jump();

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
        if (!canControl)
            return;
        HandleMovement();
        CheckGround();
    }

    private void HandleMovement()
    {
        float targetSpeed = inputX * MoveSpeed;
        float accel = Mathf.Abs(inputX) > 0.01f ? acceleration : deceleration;

        float newX = Mathf.MoveTowards(
            myRb.linearVelocity.x,
            targetSpeed,
            accel * Time.fixedDeltaTime
        );

        myRb.linearVelocity = new Vector2(newX, myRb.linearVelocity.y);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            GroundLayer
        );
    }

    private void Jump()
    {
        if (!isGrounded) return;

        myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, JumpForce);
    }

    public void PLayerDie()
    {
        RespawnAtCheckpoint();
    }

    private void RespawnAtCheckpoint()
    {
        myRb.linearVelocity = Vector2.zero;

        Vector3 spawnPos =
            CheckPointManager.Instance.GetLastCheckpointPosition(startPosition);

        float savedEnergy =
            CheckPointManager.Instance.GetSavedEnergy(EnergySystem.Instance.MaxEnergy);

        transform.position = spawnPos;
        EnergySystem.Instance.currentEnergy = savedEnergy;
    }

    public void RespawnAtStart()
    {  
        myRb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        MaskController.Instance.SwitchMask(MaskTypes.Default);
    }


    public void DisableControl()
    {
        canControl = false;
        myRb.linearVelocity = Vector2.zero;
    }

    public void EnableControl()
    {
        canControl = true;
    }
}

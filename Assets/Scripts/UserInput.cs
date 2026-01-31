using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance { get; private set; }
    [HideInInspector]public Controls Controls;
    [HideInInspector]public Vector2 MoveInput;
    [HideInInspector]public  bool JumpInput;
    public event Action<MaskTypes> MaskSwitchEvent;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }   

        Controls = new Controls();

        //Sending the value into the MoveInput variable
        Controls.Movement.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        Controls.Movement.Jump.performed += _ => JumpInput = true;
        Controls.Movement.Jump.canceled += _ => JumpInput = false;

        Controls.Movement.MaskDefault.performed += _ =>
            MaskSwitchEvent?.Invoke(MaskTypes.Default);

        Controls.Movement.MaskStone.performed += _ =>
            MaskSwitchEvent?.Invoke(MaskTypes.Stone);

        Controls.Movement.MaskFeather.performed += _ =>
            MaskSwitchEvent?.Invoke(MaskTypes.Feather);

        Controls.Movement.MaskMagnet.performed += _ =>
            MaskSwitchEvent?.Invoke(MaskTypes.Magnet);
    }

    private void OnEnable()
    {
       Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }


    public bool JumpPressedThisFrame()
    {
        return Controls.Movement.Jump.WasPressedThisFrame();
    }
}

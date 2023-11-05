using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InputManagerment : NetworkBehaviour
{
    InputManager inputSystem;
    [Header("Player Moverment")]
    public float VerticalMovermentInput;
    public float HorizontalMovermentInput;
    [SerializeField] Vector2 MovermentInput;
    [Header("Camera Rotate")]
    public float VerticalCameraInput;
    public float HorizontalCameraInput;
    [SerializeField] Vector2 CameraInput;
    private void OnEnable()
    {
        if(IsOwner)
        {
            if (inputSystem == null)
            {
                inputSystem = new InputManager();
                inputSystem.PlayerMove.Moverment.performed += i => MovermentInput = i.ReadValue<Vector2>();
                inputSystem.PlayerMove.CameraRotate.performed += i => CameraInput = i.ReadValue<Vector2>();
            }
            inputSystem.Enable();
        }
        
    }
    private void Update()
    {
       
    }
    private void OnDisable()
    {
        if (IsOwner)
            inputSystem.Disable();
    }
    public void HandleAllInput()
    {
        if (IsOwner)
        {
            HandleMovermentInput();
            HandleCameraInput();
        }
            
    }
    void HandleMovermentInput()
    {
        if (IsOwner)
        {
           HorizontalMovermentInput = MovermentInput.x;
        VerticalMovermentInput = MovermentInput.y;
        }
            
    }
    void HandleCameraInput()
    {
        if (IsOwner)
        {
            HorizontalCameraInput = CameraInput.x;
            VerticalCameraInput = CameraInput.y;
        }
        
    }
}

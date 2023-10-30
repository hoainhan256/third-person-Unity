using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerment : MonoBehaviour
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
        if (inputSystem == null)
        {
            inputSystem = new InputManager();
            inputSystem.PlayerMove.Moverment.performed += i => MovermentInput = i.ReadValue<Vector2>();
            inputSystem.PlayerMove.CameraRotate.performed += i => CameraInput = i.ReadValue<Vector2>();
        }
        inputSystem.Enable();
    }
    private void Update()
    {
       
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }
    public void HandleAllInput()
    {
        HandleMovermentInput();
        HandleCameraInput();
    }
    void HandleMovermentInput()
    {
        HorizontalMovermentInput = MovermentInput.x;
        VerticalMovermentInput = MovermentInput.y;
    }
    void HandleCameraInput()
    {
        HorizontalCameraInput = CameraInput.x;
        VerticalCameraInput = CameraInput.y;
    }
}

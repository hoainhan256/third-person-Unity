using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManagerment inputsystem;
    PlayerCamera cam;
    [SerializeField] Transform Camholder;
    [SerializeField] float RotationSpeed = 3.5f;
    Quaternion TargetRotate;
    Quaternion PlayerRotate;
    public void HandleAllLocomotion()
    {
        HandleRote();
    }
    void HandleRote()
    {
        TargetRotate = Quaternion.Euler(0, Camholder.eulerAngles.y, 0);
        PlayerRotate = Quaternion.Slerp(transform.rotation, TargetRotate, RotationSpeed * Time.deltaTime);
        if (inputsystem.VerticalMovermentInput != 0 || inputsystem.HorizontalMovermentInput != 0)
        {
            transform.rotation = PlayerRotate;
        }
    }
    private void FixedUpdate()
    {
        HandleAllLocomotion();
    }
    private void Awake()
    {
        inputsystem = GetComponent<InputManagerment>();
        cam =  FindObjectOfType<PlayerCamera>();
    }
    private void LateUpdate()
    {
        cam.CameraMove();
    }
}

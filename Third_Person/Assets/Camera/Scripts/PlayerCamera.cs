using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public InputManagerment inputSystem;
    [SerializeField] Transform CameraPivot;
    [SerializeField] GameObject Target;
    [SerializeField] Camera CamObject;
    Vector3 targetPos;
    Vector3 camfollowVelocity = Vector3.zero;
    Vector3 CameraRotation;
    Quaternion TargetRotation;
    [Header("Camera Speed")]
    [SerializeField] float CameraSmoothTime = 0.2f;
    [SerializeField] float lookAmouseVertical;
    [SerializeField] float lookAmouseHorizontal;
    [SerializeField] float MaximumPivotAngle = 15;
    [SerializeField] float MinimumPivotAngle = -15;
    private void Awake()
    {
       
    }
    public void CameraMove()
    {
        FollowTarget();
        RotateCamera();
    }
    void FollowTarget()
    {
        targetPos = Vector3.SmoothDamp(transform.position, Target.transform.position, ref camfollowVelocity, CameraSmoothTime);
        transform.position = targetPos;
    }
    void RotateCamera()
    {
        lookAmouseVertical = lookAmouseVertical + (inputSystem.HorizontalCameraInput);
        lookAmouseHorizontal = lookAmouseHorizontal - (inputSystem.VerticalCameraInput);
        lookAmouseHorizontal = Mathf.Clamp(lookAmouseHorizontal,MinimumPivotAngle,MaximumPivotAngle);

        CameraRotation = Vector3.zero;
        CameraRotation.y = lookAmouseVertical;
        TargetRotation = Quaternion.Euler(CameraRotation);
        TargetRotation = Quaternion.Slerp(transform.rotation, TargetRotation, CameraSmoothTime);
        transform.rotation = TargetRotation;

        CameraRotation = Vector3.zero;
        CameraRotation.x = lookAmouseHorizontal;
        TargetRotation = Quaternion.Euler(CameraRotation);
        TargetRotation = Quaternion.Slerp(CameraPivot.localRotation, TargetRotation, CameraSmoothTime);
        CameraPivot.localRotation = TargetRotation;
    }
}

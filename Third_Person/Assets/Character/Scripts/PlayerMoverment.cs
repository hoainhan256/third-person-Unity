using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMoverment : NetworkBehaviour
{
    InputManagerment inputSystem;
    [SerializeField] float speed;
    Animator animator;
    [SerializeField] CharacterController ccl;
    float X;
    float Y;
    [SerializeField] float Gravity = -9.8f;
    [SerializeField] float GroundGravity = -0.05f;
    [SerializeField] float Force = 500f;
    Vector3 movement;
    [SerializeField] bool IsGround = false;
    public bool isEnableCamera = false;
    [SerializeField] GameObject CameraFollow;
    private void Awake()
    {
       
            animator = GetComponent<Animator>();
            inputSystem = GetComponent<InputManagerment>();
          Debug.Log(OwnerClientId);
        
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            CameraFollow = transform.GetChild(1).gameObject;
            CameraFollow.SetActive(true);
            CameraFollow.GetComponent<ThirdPersonCamera>().target = this.transform;
        }
        base.OnNetworkSpawn();
    }
    void Start()
    {
        movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
           isEnableCamera = true;
           
            X = Input.GetAxis("Horizontal") * speed;
            Y = Input.GetAxis("Vertical") * speed;
            Moverment();
            if (Input.GetKeyDown(KeyCode.Space) && IsGround)
            {
                ccl.Move(new Vector3(0, Force * Time.deltaTime, 0));

            }

            IsGround = ccl.isGrounded;
        }
       
        
    }
    void Moverment()
    {
        if(IsOwner)
        {
            movement.x = X;
            movement.z = Y;
            if (IsGround)
            {
                movement.y = GroundGravity;
            }
            else
            {
                movement.y = Gravity;
            }
            movement = transform.TransformDirection(movement) * speed;
            ccl.Move(movement * Time.deltaTime);


            animator.SetFloat("X", X);
            animator.SetFloat("Y", Y);

            if (X == 0 && Y == 0)
            {
                animator.SetBool("Move", false);
                return;
            }
            animator.SetBool("Move", true);
            if(NetworkManager.Singleton.IsClient)
            {
                //UpdatePositionOnServerClientRpc(transform.position);
            }
            
        }

    }
    //[ClientRpc]
    //private void UpdatePositionOnServerClientRpc(Vector3 position)
    //{
    //    if (IsServer || IsHost)
    //    {
            
    //        transform.position = position;
    //    }
    //}
}

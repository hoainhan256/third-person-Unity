using UnityEngine;
using Unity.Netcode;
using static UnityEngine.GraphicsBuffer;

public class ThirdPersonCamera : NetworkBehaviour
{
    public Transform target; // Đối tượng người chơi
    public float sensitivity = 2.0f; // Độ nhạy của chuột
    public float rotationSpeed = 5.0f; // Tốc độ xoay camera
    public float distance = 5.0f; // Khoảng cách ban đầu giữa camera và người chơi
    public float height = 2.0f; // Chiều cao ban đầu của camera
    [SerializeField] float PlayerRotateSpeed = 3.5f;
    [SerializeField] float Offset = 2f;
    private float rotationX = 0;
    private float rotationY = 0;
    [SerializeField] Transform Camera;
    [SerializeField] bool TypeCamera = true;
    [SerializeField] bool isLockedMouse = true;
    private GameObject[] players;
    private void Awake()
    {
       
           
            Camera = GetComponentInChildren<Camera>().transform;
        
        
        Offset = 0.5f;
        distance = 3f;
        height = 2f;
       
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Debug.Log(OwnerClientId);

    }

    void LateUpdate()
    {   
        
        
            if (Input.GetKeyDown(KeyCode.V))
            {
                TypeCamera = !TypeCamera;
            }
            if (TypeCamera)
            {
                Offset = 0.5f;
                distance = 3f;
                height = 2f;
            }
            else
            {
                Offset = 0;
                distance = 0;
                height = 1.75f;
            }
            // Xử lý chuột
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY * rotationSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -45, 45);

            rotationY += mouseX * rotationSpeed * Time.deltaTime;
            
            // Quay camera theo con trỏ chuột
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
            Quaternion TargetRotate = Quaternion.Euler(0, rotationY, 0);
            Quaternion PlayerRotate = Quaternion.Slerp(target.rotation, TargetRotate, PlayerRotateSpeed * Time.deltaTime);
            //player.rotation = Quaternion.Euler(0, rotationY, 0);
            target.rotation = PlayerRotate;

            // Xử lý di chuyển camera
            Vector3 offset = Camera.transform.localPosition;
            offset.x = Offset;
            Camera.transform.localPosition = offset;
            Vector3 desiredPosition = target.position - transform.forward * distance;
            desiredPosition.y = target.position.y + height;
            transform.position = desiredPosition;
        
    }
    private void Update()
    {
       
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isLockedMouse = !isLockedMouse;
                if(isLockedMouse)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        
    }
}


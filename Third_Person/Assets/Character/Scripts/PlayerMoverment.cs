using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    InputManagerment inputSystem;
    [SerializeField] float speed;
    Animator animator;
    Rigidbody rb;
    float X;
    float Y;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputManagerment>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputSystem.HandleAllInput();
        X = inputSystem.HorizontalMovermentInput * speed;
        Y = inputSystem.VerticalMovermentInput * speed;
        Moverment();
    }
    void Moverment()
    {
        if(X == 0 && Y == 0)
        {
            animator.SetBool("Move", false);
            return;
        }
        Vector3 movement = new Vector3(X, 0.0f, Y);
        movement = transform.TransformDirection(movement);
        rb.velocity = movement * speed;
        animator.SetBool("Move", true);
        animator.SetFloat("X", X);
        animator.SetFloat("Y", Y);

    }
}

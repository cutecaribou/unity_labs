using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    // public CharacterController controller;
    public float movementSpeed;
    public float jumpSpeed;
    private Vector3 moveDirection;
    public bool isOnGround = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
         
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 5f * Time.deltaTime);
            rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
            // controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        } 

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            Jump();
            isOnGround = false;
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        // moveDirection.y = jumpSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movementSpeed;
    public float jumpSpeed;
    private Vector3 moveDirection;
    public bool isOnGround = true;
    private Animator animator;
    public PlayerState playerHealth;
    bool fallDamageFlag = false;

    Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Physics.gravity = new Vector3 (0, -15, 0);
    }

    void Update()
    {
        
        if (playerHealth.currentHealth <= 0){
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        moveDirection.Normalize();
         
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 5f * Time.deltaTime);
            // rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

            animator.SetBool("isMoving", true);
        } 
        else
        {
            animator.SetBool("isMoving", false);  
        }

        rb.velocity = moveDirection * movementSpeed + new Vector3(0f, rb.velocity.y, 0f);
  
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            Jump();        
        }

        if (isOnGround)
        {
            animator.SetBool("isJumping", false);
        }

        if (fallDamageFlag && rb.position.y >= -1) {
            fallDamageFlag = false;
            playerHealth.TakeDamage(1);
        }

        if (rb.position.y < -10) {
            fallDamageFlag = true;
            rb.velocity = new Vector3(0f, 0f, 0f);
            gameObject.transform.position = lastPosition + new Vector3(0f, 2f, 0f);
        }
    }

    public void Jump()
    {
        if (isOnGround)
        {
            isOnGround = false;
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            animator.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            if (!collision.gameObject.GetComponent("WaypointFollower")) {
                lastPosition = collision.transform.position;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

}

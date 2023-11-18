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

    public PlayerHealth playerHealth;

    Vector3 lastPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        moveDirection.Normalize();
         
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 5f * Time.deltaTime);
            // rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        } 
        rb.velocity = moveDirection * movementSpeed + new Vector3(0f, rb.velocity.y, 0f);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            Jump();
            isOnGround = false;
        }

        if (rb.position.y < -10) {
            if (playerHealth)
                playerHealth.TakeDamage(1);
            rb.velocity = new Vector3(0f, 0f, 0f);
            gameObject.transform.position = lastPosition + new Vector3(0f, 2f, 0f);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
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
}

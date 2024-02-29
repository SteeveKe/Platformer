using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 30f;
    public bool isGrounded;
    public float jumpBoost = 3f;
    public float mouvement;

    private Rigidbody rb;
    private Collider col;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        mouvement = horizontalMovement;
        rb.velocity += Vector3.right * horizontalMovement * acceleration * Time.deltaTime;
        
        if (Math.Abs(rb.velocity.x) > maxSpeed)
        {
            Vector3 newvel = rb.velocity;
            newvel.x = Math.Clamp(newvel.x, -maxSpeed, maxSpeed);
            rb.velocity = newvel;
        }

        Jump();
        Rotation();
        Animation();
        
        
    }

    private void Jump()
    {
        var bounds = col.bounds;
        float halfHeight = bounds.center.y - bounds.min.y + 0.5f;

        Vector3 startPoint = bounds.center;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
        }
    }
    private void Rotation()
    {
        float yaw = rb.velocity.x > 0 ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    private void Animation()
    {
        float speed = Math.Abs(rb.velocity.x);
        anim.SetFloat("Speed", speed);
        anim.SetBool("InAir", !isGrounded);
    }
}

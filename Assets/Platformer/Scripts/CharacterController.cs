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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.velocity += Vector3.right * horizontalMovement * acceleration * Time.deltaTime;
        
        if (Math.Abs(rbody.velocity.x) > maxSpeed)
        {
            Vector3 newvel = rbody.velocity;
            newvel.x = Math.Clamp(newvel.x, -maxSpeed, maxSpeed);
            rbody.velocity = newvel;
            //rbody.velocity = rbody.velocity.normalized * maxSpeed;
        }

        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.03f;
        
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, 2f);
        Color lineColor = isGrounded ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space))
        {
            if (rbody.velocity.y > 0)
            {
                rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
        }

        float yaw = rbody.velocity.x > 0 ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
    
}

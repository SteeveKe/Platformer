using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Mouvement : MonoBehaviour
{
    public float acceleration = 10f;
    public float decceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 30f;
    public bool isGrounded;
    public float jumpBoost = 3f;
    public float apexThreshold = 1f;
    public float apexMul = 2f;
    public float apexMax = 4f;
    public float jumpBuffer = 0.5f;
    public float cayoteTime = 1f;
    public float maxFallspeed;

    public float cayoteTimerJump = 0f;
    private bool canJumpBuffer;
    private bool isJumping; 
    private float LastOnGroundTime;
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
	    cayoteTimerJump -= Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal");
      
        Jump();
        Rotation();
        Animation();
        Walk(horizontalMovement);
        FallSpeed();
    }

    private void FallSpeed()
    {
	    if (rb.velocity.y < -maxSpeed )
	    {
		    Vector3 fall = rb.velocity;
		    fall.y = -maxFallspeed;
		    rb.velocity = fall;
	    }    
    }
    
    private void Walk(float horizontalMovement)
    {
	    float targetSpeed = horizontalMovement * maxSpeed;
	    float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        
	    if (!isGrounded && Mathf.Abs(rb.velocity.y) < apexThreshold)
	    {
		    accelRate *= apexMul;
		    targetSpeed *= apexMax;
	    }
		
	    float speedDif = targetSpeed - rb.velocity.x;
	    float movement = speedDif * accelRate;
	    rb.AddForce(movement * Vector2.right, ForceMode.Force);
    }
    private void Jump()
    {
        var bounds = col.bounds;
        float halfHeight = bounds.center.y - bounds.min.y + 0.3f;

        Vector3 startPoint = bounds.center;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        canJumpBuffer = Physics.Raycast(startPoint, Vector3.down, halfHeight + jumpBuffer);
        isJumping = !isGrounded;
        Color lineColor = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);

        if (isGrounded)
        {
	        cayoteTimerJump = cayoteTime;
        }
        
        if ((canJumpBuffer || cayoteTimerJump > 0) && Input.GetKeyDown(KeyCode.Space))
        {
	        if (isGrounded)
	        {
		        cayoteTimerJump = -1;
		        isJumping = true;
		        rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
	        }
	        else
	        {
		        isJumping = true;
		        Vector3 f = rb.velocity;
		        f.y = 0;
		        rb.velocity = f;
		        rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
	        }
	        
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
	    if (Math.Abs(rb.velocity.x) > 1)
	    {
		    float yaw = rb.velocity.x > 0 ? 90 : -90;
		    transform.rotation = Quaternion.Euler(0f, yaw, 0f);
	    }
    }

    private void Animation()
    {
        float speed = Math.Abs(rb.velocity.x);
        anim.SetFloat("Speed", speed);
        anim.SetBool("InAir", !isGrounded);
    }
}

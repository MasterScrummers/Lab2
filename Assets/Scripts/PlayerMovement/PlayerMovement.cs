using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;


    public float jumpForce; //How High Player Jump
    public float jumpTime;  //Maxmium Time on air
    public float jumpTimeCounter; //Counter for JumpTime

    public bool grounded; //boolean to define if the player is on the ground or not
    public LayerMask Ground; //A LayerMask which defines what is ground object
    public bool stoppedJumping = true; //Boolean to define if the player stops jumping

    public Transform bottom; //Mario's feet, to check if it is colliding with the ground
    public Transform top; //Mario's head
    public float radius; //the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        jumpTimeCounter = jumpTime; //Set Max jump time = counter
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.Log("Can't find animator");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Cant find Rigidbody");
        }
    }

    void Update()
    {
        Jump();
        Move();

    }

    void FixedUpdate()
    {

    }

    void Move()
    {
        float forward = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(forward, 0.0f);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
        if (animator.GetBool("IsJumping") == false)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("IsMoving", true);
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("IsMoving", true);
            }

            if (Input.GetAxis("Horizontal") == 0)
            {
                animator.SetBool("IsMoving", false);
            }
        }

        else if (animator.GetBool("IsJumping") == true)
        {
            animator.SetBool("IsMoving", false);
        }

    }
    void Jump()
    {
        grounded = Physics2D.OverlapCircle(bottom.position, radius, Ground); //Physics2D.OverlapCircle returns true if the bottom circle collide with the ground layerMask.


        if (grounded)
        {
            jumpTimeCounter = jumpTime; //Reset Jump Counter
            animator.SetBool("IsJumping", false);
        }

        // If Player Press the space bar
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            //And if Mario is on the ground
            if (grounded)
            {
                //Then Jump
                rb.velocity = new Vector2(rb.velocity.y, jumpForce) ;
                stoppedJumping = false;
                animator.SetBool("IsJumping", true);
            }
        }

        //If Player is holding space bar
        if (((Input.GetKey(KeyCode.Space)) || Input.GetKey(KeyCode.W))&& !stoppedJumping)
        {
            //And if the counter is > 0
            if (jumpTimeCounter > 0)
            {
                //Jump Higher, at the same time start the counter
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                animator.SetBool("IsJumping", true);
            }
        }

        // If Player release space bar
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            //Stop Jumping 
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(bottom.position, radius);
        Gizmos.DrawSphere(top.position, radius);
    }      
              
}

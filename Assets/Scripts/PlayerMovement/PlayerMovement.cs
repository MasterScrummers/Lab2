using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MarioSpriteUpdator))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

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
    private InputController input;

    void Start()
    {
        jumpTimeCounter = jumpTime; //Set Max jump time = counter
        rb = GetComponent<Rigidbody2D>();
        input = DoStatic.GetGameController().GetComponent<InputController>();
    }

    void Update()
    {
        Jump();
        readInput();
    }

    void readInput()
    {
        if (input.vertical != -1)
        {
            float direction = input.horizontal;
            Move(direction);
        }
    }

    public void Move(float hDirection)
    {
        rb.AddForce(new Vector2(hDirection, 0), ForceMode2D.Impulse);
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -speed, speed);
        rb.velocity = currentVelocity;
    }

    void Jump()
    {
        grounded = Physics2D.OverlapCircle(bottom.position, radius, Ground); //Physics2D.OverlapCircle returns true if the bottom circle collide with the ground layerMask.
        if (grounded)
        {
            jumpTimeCounter = jumpTime; //Reset Jump Counter
        }

        // If (Player Press the space bar or w) And Mario is on the ground
        if ((input.jump || input.vertical > 0) && grounded)
        {
            //Then Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce) ;
            stoppedJumping = false;
        }

        //If Player is holding space bar
        if ((input.jump || input.vertical > 0) && !stoppedJumping)
        {
            //And if the counter is > 0
            if (jumpTimeCounter > 0)
            {
                //Jump Higher, at the same time start the counter
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // If Player release space bar
        if (input.jump || input.vertical > 0)
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

using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class MarioSpriteUpdator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerMovement movement;

    private InputController input;
    private PlayerState playerState;
    public int PowerState = 0;

    private BoxCollider2D head;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        input = DoStatic.GetGameController().GetComponent<InputController>();
        playerState = DoStatic.GetGameController().GetComponent<PlayerState>();

        head = GameObject.FindGameObjectWithTag("head").GetComponent<BoxCollider2D>();
        UpdateCollider();
    }

    void Update()
    {
        animator.SetBool("IsCrouching", input.vertical < 0);
        animator.SetBool("IsMoving", input.horizontal != 0);
        animator.SetInteger("PowerState", PowerState);
        animator.SetBool("IsJumping", !movement.isOnGround);

        if (input.horizontal != 0)
        {
            sprite.flipX = input.horizontal < 0;
        }

        if (PowerState == 2 && Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void ChangePowerState(int amount)
    {
        if (amount < 0 && PowerState == 0)
        {
            animator.SetTrigger("Death");
            playerState.TriggerDeath(true);
        } else
        {
            PowerState = Mathf.Clamp(PowerState + amount, 0, 2);
        }
    }

    public void Respawn() {
        animator.SetTrigger("Respawn");
    }

    /// <summary>
    /// This is used according to the animation.
    /// </summary>
    private void FireAttack()
    {
        Debug.Log("Attacking!");
    }

    /// <summary>
    /// This is used according to the animation,
    /// </summary>
    /// 

    //Add this function after the frame of different animation, e.g Crouch, Upgrade Animation, Downgarde Animation
    //So after those animation, the collider will re-size accordingly
    private void UpdateCollider()
    {
        //Destory BoxCollider of Mario
        Destroy(GetComponent<BoxCollider2D>());

        //Re-add the BoxCollider
        BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
        
        //size = Re-added Box Collider's size
        Vector2 size = bc.size;

        //Reduce the size the collider by 80%, so it will be the same in the orginal game
        size.x *= 0.8f;
        bc.size = size;

        //Ref to head's offset
        Vector2 headOffset = head.offset;

        //headOffset will always be placed on top of the collider,
        headOffset.y = bc.size.y;

        //Apply headOffset to actual collider, so that it will change accordingly
        head.offset = headOffset;
    }
}

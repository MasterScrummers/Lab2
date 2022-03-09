using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class MarioSpriteUpdator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    private InputController input;
    private AudioController audioController;
    private PlayerState playerState;
    private PlayerMovement movement;
    public int PowerState = 0;


    private BoxCollider2D head;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        GameObject controller = DoStatic.GetGameController();
        input = controller.GetComponent<InputController>();
        audioController = controller.GetComponent<AudioController>();
        playerState = controller.GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();

        head = GameObject.FindGameObjectWithTag("head").GetComponent<BoxCollider2D>();
        UpdateCollider();
    }

    void Update()
    {
        animator.SetBool("IsCrouching", input.vertical < 0);
        animator.SetBool("IsMoving", input.horizontal != 0);
        animator.SetBool("IsJumping", !movement.isOnGround);
        animator.SetInteger("PowerState", PowerState);

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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("fallingBoundary")) {
            playerState.TriggerDeath(false);
        }
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
    private void UpdateCollider()
    {
        Destroy(GetComponent<BoxCollider2D>());
        BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
        Vector2 size = bc.size;
        size.x *= 0.8f;
        bc.size = size;

        Vector2 headOffset = head.offset;
        headOffset.y = bc.size.y;
        head.offset = headOffset;
    }

    private void PowerUp()
    {
        audioController.PlaySound("Powerup");
    }

    private void Hurt()
    {
        audioController.PlaySound("Pipe Travel");
    }
}

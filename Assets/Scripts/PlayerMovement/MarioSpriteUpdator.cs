using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class MarioSpriteUpdator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    private InputController input;
    private PlayerState playerState;
    public int PowerState = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        input = DoStatic.GetGameController().GetComponent<InputController>();
        playerState = DoStatic.GetGameController().GetComponent<PlayerState>();
    }

    void Update()
    {
        animator.SetBool("IsCrouching", input.vertical < 0);
        animator.SetBool("IsMoving", input.horizontal != 0);
        animator.SetInteger("PowerState", PowerState);
        if (input.horizontal != 0)
        {
            sprite.flipX = input.horizontal < 0;
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
}

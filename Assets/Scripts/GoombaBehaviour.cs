using System.Collections;
using UnityEngine;

//Do not require RigidBody2D
[RequireComponent(typeof(Animator))]
public class GoombaBehaviour : MonoBehaviour
{
    public int direction = -1;
    public float movementSpeed = 6;
    public float bounceStrength = 5;
    public float timeBeforeRemoval = 1; //Is in seconds.
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * movementSpeed;
        if (rb.velocity.x < 0.01)
        {
            movementSpeed *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("ground"))
        {
            //direction *= -1;
        } else  if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<MarioSpriteUpdator>().ChangePowerState(-2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("foot"))
        {
            GetComponent<Animator>().SetTrigger("Death");
            DoStatic.GetGameController().GetComponent<AudioController>().PlaySound("Goomba Stomp");
            collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, bounceStrength));
            movementSpeed = 0;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponentInParent<Rigidbody2D>());
            StartCoroutine(DeathAnimation());
        }

        if (collision.tag == "FireBall")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSecondsRealtime(timeBeforeRemoval);
        Destroy(gameObject);
    }
}

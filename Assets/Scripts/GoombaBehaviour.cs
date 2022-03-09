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
        //transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * movementSpeed;
        if (rb)
        {
            Vector2 vel = rb.velocity;
            vel.x = direction * movementSpeed;
            rb.velocity = vel;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("ground"))
        {
            //direction *= -1;
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<MarioSpriteUpdator>().setPowerState(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "foot":
                GetComponent<Animator>().SetTrigger("Death");
                DoStatic.GetGameController().GetComponent<AudioController>().PlaySound("Goomba Stomp");
                collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, bounceStrength), ForceMode2D.Impulse);
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponentInParent<Rigidbody2D>());
                movementSpeed = 0;
                StartCoroutine(DeathAnimation());
                return;

            case "fallingBoundary":
            case "FireBall":
                DestorySelf();
                return;
        }
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSecondsRealtime(timeBeforeRemoval);
        DestorySelf();
    }

    private void DestorySelf()
    {
        DoStatic.GetGameController().GetComponent<VariableController>().score += 100;
        Destroy(gameObject);
    }
}

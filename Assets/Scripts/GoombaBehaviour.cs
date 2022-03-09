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

    void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("ground"))
        {
            //direction *= -1;
        } else  if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<MarioSpriteUpdator>().setPowerState(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("foot"))
        {
            GetComponent<Animator>().SetTrigger("Death");
            DoStatic.GetGameController().GetComponent<AudioController>().PlaySound("Goomba Stomp");
            collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, bounceStrength), ForceMode2D.Impulse);
            movementSpeed = 0;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponentInParent<Rigidbody2D>());
            StartCoroutine(DeathAnimation());
        }

        if (collision.tag == "FireBall")
        {

            DestorySelf();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    private Animator animator;
    //Need Change
    float speed = 10.0f;
    private float lifeTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletLifeTime();
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        if (hitinfo.tag != "Player" && hitinfo.tag != "FireBall")
        {
            animator.SetTrigger("isHit");
            Destroy(GetComponent<Rigidbody2D>());


        }
    }

    private void DestorySelf()
    {
        DoStatic.GetGameController().GetComponent<VariableController>().score += 500;
        Destroy(gameObject);
        
    }

    public void SetVelocity(bool RDirection)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        if (!RDirection)
        {
            rb.velocity *= -1;
        }
        

    }
    void BulletLifeTime()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);

        }
    }

}

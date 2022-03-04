using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    int direction; 

    void Start()
    {
        direction = -1; //left is -1, right is 1.
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;
    }

    void DeathSequence()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("terrain"))
        {
            direction *= -1;

        }else if (collision.gameObject.CompareTag("foot"))
        {
            DeathSequence();
        }
    }
}

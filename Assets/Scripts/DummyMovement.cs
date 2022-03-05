using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
          
            //Debug.Log("Moving left");
            Vector3 pos = this.transform.position;
            pos.x -= 1 * speed;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
           


            Vector3 pos = this.transform.position;
            pos.x += 1 * speed;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 pos = this.transform.position;
            pos.y += 1 * speed;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 pos = this.transform.position;
            pos.y -= 1 * speed;
            this.transform.position = pos;
        }
    }
}

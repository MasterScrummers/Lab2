using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStomp : MonoBehaviour
{
    private Rigidbody2D parentRigid;
    // Start is called before the first frame update
    void Start()
    {
        parentRigid = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hurtbox")
        {
            collision.gameObject.GetComponent<StompScript>().StompSequence();
            parentRigid.AddForce(new Vector2(0, 400));
        }
    }
}

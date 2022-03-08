using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }

    public void StompSequence()
    {
        Debug.Log("Hit");
        DeathSequence();
    }
    void DeathSequence()
    {
        Destroy(transform.parent.gameObject);
    }
}

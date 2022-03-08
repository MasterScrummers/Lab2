using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField] GameObject playerState;
    [SerializeField] GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        playerState = GameObject.FindWithTag("deathrespawncontroller");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            Debug.Log("Pressed w");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        {
            if (other.gameObject.CompareTag("enemy")) {
                playerState.GetComponent<PlayerState>().TriggerDeath(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("fallingBoundary")) {
            playerState.GetComponent<PlayerState>().TriggerDeath(false);
        } else if (other.CompareTag("flagPole")) {
            flag.GetComponent<FlagEnding>().TriggerFlagEnding();
        }
    }
}

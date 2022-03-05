using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomBehaviour : MonoBehaviour
{

    int direction;
    float speed;
    GameObject GameState;
    gameStates gameState;

    void Start()
    {
        direction = -1; //left is -1, right is 1.
        speed = 6;
        GameState = GameObject.Find("gameStatusTracker");
        gameState = (gameStates)GameState.GetComponent(typeof(gameStates));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * speed;
    }

    void DeathSequence()
    {
        if (gameState) {
            gameState.marioPowerUp(1);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("terrain"))
        {
            direction *= -1;

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            DeathSequence();
        }
    }
}

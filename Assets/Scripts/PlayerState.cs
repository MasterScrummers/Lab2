using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerRigidbody2D;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Sprite deathSprite;

    const float deathLength = 5.0f;
    float deathTimer;
    bool deathSeq;
    
    const int max_lives = 9;
    int currentLives;

    void Awake() 
    {
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        deathSeq = false;
        deathTimer = deathLength;

        currentLives = max_lives;
        TriggerDeath(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (deathSeq) {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0) {
                if (currentLives >= 1) {
                    TriggerRespawn();  
                } else {
                    TriggerGameOver();
                }
            }
        }
    }

    public void TriggerDeath(bool playDeathAnim) {
        
        player.GetComponent<DummyController>().enabled = false;
        deathSeq = true;
        player.GetComponent<SpriteRenderer>().sprite = deathSprite;

        if (playDeathAnim)
        {
            StartDeathAnim();
        }
    }

    void StartDeathAnim() {
        playerRigidbody2D.AddForce(new Vector2(0f, 400));
    }

    public void TriggerRespawn() {
        
        deathSeq = false;

        Destroy(player);
        Instantiate(playerPrefab);
        currentLives -= 1;
    }

    void TriggerGameOver() {
        Debug.Log("Game over");
    }
}

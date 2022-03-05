using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerRigidbody2D;

    [SerializeField] Sprite deathSprite;

    const float deathLength = 5.0f;
    float deathTimer;

    bool deathSeq;
    
    void Awake() 
    {
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        deathSeq = false;
        deathTimer = deathLength;
        TriggerDeath(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (deathSeq) {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0) {
                TriggerRespawn();
            }
        }
    }

    public void TriggerDeath(bool playDeathAnim) {
        
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
    }
}

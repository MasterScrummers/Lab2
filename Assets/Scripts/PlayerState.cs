using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRigidbody2D;

    // [SerializeField] GameObject playerPrefab;
    // [SerializeField] Sprite deathSprite;

    private VariableController varController;
    private AudioController audioController;

    Vector3 playerStartPos = new Vector3(-8.38f, -2.56f);

    const float deathLength = 5.0f;
    float deathTimer;
    bool deathSeq;
    
    int currentLives;
    
    // Start is called before the first frame update
    void Start()
    {
        player = DoStatic.GetPlayer();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();

        varController = GetComponent<VariableController>();
        audioController = GetComponent<AudioController>();

        deathSeq = false;
        deathTimer = deathLength;
        currentLives = varController.ResetLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathSeq) {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0) {
                if (currentLives > 1) {
                    TriggerRespawn();  
                } else {
                    TriggerGameOver();
                }
            }
        }
    }

    public async void TriggerDeath(bool playDeathAnim) {

        audioController.StopMusic();
        audioController.PlaySound("Death");

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<MarioSpriteUpdator>().enabled = false;

        Transform[] children = DoStatic.GetChildren(player.transform);
        foreach (Transform child in children) {
            child.GetComponent<BoxCollider2D>().enabled = false;
        }

        deathSeq = true;

        if (playDeathAnim)
        {
            StartDeathAnim();
        }
    }

    void StartDeathAnim() {
        player.GetComponent<BoxCollider2D>().enabled = false;
        playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.angularVelocity = 0f;
        playerRigidbody2D.AddForce(new Vector2(0f, 400));
    }

    public void TriggerRespawn() {
        deathSeq = false;
        deathTimer = deathLength;

        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<MarioSpriteUpdator>().enabled = true;

        Transform[] children = DoStatic.GetChildren(player.transform);
        foreach (Transform child in children) {
            child.GetComponent<BoxCollider2D>().enabled = true;
        }

        player.GetComponent<MarioSpriteUpdator>().Respawn();

        GetComponent<SceneController>().ChangeScene("Overworld", playerStartPos);

        currentLives = varController.DecrementLife();
    }

    void TriggerGameOver() {
        Debug.Log("Game over");
    }
}

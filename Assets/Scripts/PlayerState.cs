using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private GameObject player;
    private GameObject gameController;
    private Rigidbody2D playerRigidbody2D;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Sprite deathSprite;

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

        gameController = DoStatic.GetGameController();
        varController = gameController.GetComponent<VariableController>();
        audioController = gameController.GetComponent<AudioController>();

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

    public void TriggerDeath(bool playDeathAnim) {

        audioController.StopMusic();
        audioController.PlaySound("Death");
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
        player.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void TriggerRespawn() {
        deathSeq = false;
        deathTimer = deathLength;

        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<DummyController>().enabled = true;

        gameController.GetComponent<SceneController>().ChangeScene("Overworld", playerStartPos);
        //change sprite back to normal mario
        currentLives = varController.DecrementLife();
    }

    void TriggerGameOver() {
        Debug.Log("Game over");
    }
}

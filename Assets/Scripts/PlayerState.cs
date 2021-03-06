using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRigidbody2D;

    [SerializeField] UITimer timer;

    private VariableController varController;
    private AudioController audioController;

    Vector3 playerStartPos = new Vector3(-4.55f, 0f, 0f);

    const float deathLength = 5.0f;
    float deathTimer;
    bool deathSeq;
    
    int currentLives;

    public Vector3 setCamPos;
    
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
        setCamPos = new Vector3 (0, 0, -10);
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

        timer.PauseTimer(true);
        audioController.StopMusic();
        audioController.PlaySound("Death");

        playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.angularVelocity = 0f;

        EnableColliders(false);

        deathSeq = true;

        if (playDeathAnim)
        {
            StartDeathAnim();
        }
    }

    void StartDeathAnim() {
        playerRigidbody2D.AddForce(new Vector2(0f, 40), ForceMode2D.Impulse);
        
        Vector2 vel = playerRigidbody2D.velocity;
        vel.y = vel.y > 25 ? 25 : vel.y;
        playerRigidbody2D.velocity = vel;

        playerRigidbody2D.gravityScale = 5;
    }

    public void TriggerRespawn() {
        deathSeq = false;
        deathTimer = deathLength;

        EnableColliders(true);

        player.GetComponent<MarioSpriteUpdator>().Respawn();

        GetComponent<SceneController>().ChangeScene("Overworld", playerStartPos, setCamPos);

        timer.ResetTimer();
        timer.PauseTimer(false);
        currentLives = varController.DecrementLife();
        varController.ResetScore();
    }

    void TriggerGameOver() {
        Debug.Log("Game over");
    }

    void EnableColliders(bool enable)
    {
        player.GetComponent<BoxCollider2D>().enabled = enable;
        player.GetComponent<PlayerMovement>().enabled = enable;
        player.GetComponent<MarioSpriteUpdator>().enabled = enable;

        Transform[] children = DoStatic.GetChildren(player.transform);
        
        foreach (Transform child in children) {
            if (child.TryGetComponent<BoxCollider2D>(out BoxCollider2D box)) {
                box.enabled = enable;
            }
        }
    }
}

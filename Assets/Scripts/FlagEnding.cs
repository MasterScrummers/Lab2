using System.Collections;
using UnityEngine;

public class FlagEnding : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject flag;

    GameObject gameController;
    AudioController audioController;
    [SerializeField] UITimer timer;
    GameObject mainCamera;

    Vector2 bottomOfFlag = new Vector2(189f, -1.3f);
    Vector2 playerFlagBottom = new Vector2(189f, -2f);
    Vector2 endingDoor = new Vector2 (195.5f, -2.5f);

    const float flagTime = 1.0f;
    Tween flagTween;
    Tween marioFlagTween;
    Tween marioTween;

    float flagLength;

    Coroutine marioRoutine;
    bool marioWalk;


    // Start is called before the first frame update
    void Start()
    {
        player = DoStatic.GetPlayer();
        gameController = DoStatic.GetGameController();
        audioController = gameController.GetComponent<AudioController>();

        Transform[] gameControllerChildren = DoStatic.GetChildren(gameController.transform);
        foreach (Transform child in gameControllerChildren) {
            if (child.CompareTag("MainCamera")) {
                mainCamera = child.gameObject;
            }
        }

        flagLength = Vector2.Distance(flag.transform.position, bottomOfFlag);
        marioWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagTween != null)
        {
            if (Vector3.Distance(flagTween.Target.position, flagTween.EndPos) > 0.01f) {
                float timeFraction = (Time.time - flagTween.StartTime) / flagTween.Duration;
                flagTween.Target.position = Vector3.Lerp(flagTween.StartPos, flagTween.EndPos, timeFraction);
            } else {
                flagTween.Target.position = flagTween.EndPos;
                flagTween = null;

                marioRoutine = StartCoroutine(TriggerMarioMovement());
            }
        }

        if (marioFlagTween != null)
        {
            if (Vector3.Distance(marioFlagTween.Target.position, marioFlagTween.EndPos) > 0.0001f) {
                float timeFraction = (Time.time - marioFlagTween.StartTime) / marioFlagTween.Duration;
                marioFlagTween.Target.position = Vector3.Lerp(marioFlagTween.StartPos, marioFlagTween.EndPos, timeFraction);
            } else {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<Rigidbody2D>().angularVelocity = 0f;

                marioFlagTween.Target.position = marioFlagTween.EndPos;
                marioFlagTween = null;
            }
        }

        if (marioWalk) {
            player.GetComponent<PlayerMovement>().Move(1);

            if (player.transform.position.x >= endingDoor.x) {
                player.SetActive(false);
                marioWalk = false;
            }
        }
    }

    public void TriggerFlagEnding() {
        
        StartCoroutine(PlayMusic());
        timer.PauseTimer(true);

        player.GetComponent<MarioSpriteUpdator>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        AddFlagTween();
        AddMarioFlagTween();
    }

    IEnumerator TriggerMarioMovement() {

        mainCamera.GetComponent<CamFollow>().enabled = false;
        yield return new WaitUntil(() => marioFlagTween == null);

        yield return new WaitForSeconds(0.05f);

        player.transform.position = new Vector2(190f, player.transform.position.y);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);

        yield return new WaitForSeconds(1);

        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Animator>().Play("Base Layer.Cutscene Walk.Small_Mario_Walking", 0);
        marioWalk = true;

        yield break;
    }

    IEnumerator PlayMusic() {
        audioController.StopMusic();
        audioController.PlaySound("Flagpole");

        yield return new WaitForSeconds(1.172f);

        audioController.PlaySound("Stage Clear");

        yield break;
    }

    void AddFlagTween () {
        if (flagTween == null) {
            flagTween = new Tween (flag.transform, flag.transform.position, bottomOfFlag, Time.time, flagTime);
        }
    }

    void AddMarioFlagTween() {
        float playerDistToBottomOfFlag = Vector2.Distance(player.transform.position, playerFlagBottom);
        if (marioFlagTween == null) {
            marioFlagTween = new Tween(player.transform, player.transform.position, playerFlagBottom, Time.time, (playerDistToBottomOfFlag/flagLength)*flagTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            TriggerFlagEnding();
        }
    }
}

using System.Collections;
using UnityEngine;

public class FlagEnding : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Sprite slideSprite;
    [SerializeField] Sprite normalSprite;

    [SerializeField] GameObject gameController;
    AudioController audioController;

    Vector2 bottomOfFlag = new Vector2(189f, -1.3f);
    Vector2 endingDoor = new Vector2 (195.5f, -2.5f);

    const float flagTime = 1.0f;
    Tween flagTween;
    Tween marioFlagTween;
    Tween marioTween;

    float flagLength;

    Coroutine marioRoutine;

    // Start is called before the first frame update
    void Start()
    {
        audioController = gameController.GetComponent<AudioController>();
        flagLength = Vector2.Distance(transform.position, bottomOfFlag);
        //TriggerFlagEnding();
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
            if (Vector3.Distance(marioFlagTween.Target.position, marioFlagTween.EndPos) > 0.01f) {
                float timeFraction = (Time.time - marioFlagTween.StartTime) / marioFlagTween.Duration;
                marioFlagTween.Target.position = Vector3.Lerp(marioFlagTween.StartPos, marioFlagTween.EndPos, timeFraction);
            } else {
                marioFlagTween.Target.position = marioFlagTween.EndPos;
                marioFlagTween = null;
            }
        }

        if (marioTween != null)
        {
            if (Vector3.Distance(marioTween.Target.position, marioTween.EndPos) > 0.01f) {
                float timeFraction = (Time.time - marioTween.StartTime) / marioTween.Duration;
                marioTween.Target.position = Vector3.Lerp(marioTween.StartPos, marioTween.EndPos, timeFraction);
            } else {
                StopCoroutine(marioRoutine);
                marioTween.Target.position = marioTween.EndPos;
                marioTween = null;
                Destroy(player);
            }
        }
    }

    public void TriggerFlagEnding() {
        audioController.StopMusic();
        audioController.PlaySound("Stage Clear");

        player.GetComponent<DummyController>().enabled = false;
        player.GetComponent<SpriteRenderer>().sprite = slideSprite;

        AddFlagTween();
        AddMarioFlagTween();
    }

    IEnumerator TriggerMarioMovement() {
        player.transform.position = new Vector2(190f, player.transform.position.y);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);

        yield return new WaitForSeconds(1);

        player.GetComponent<SpriteRenderer>().sprite = normalSprite;
        AddMarioTween();

        yield break;
    }

    void AddFlagTween () {
        if (flagTween == null) {
            flagTween = new Tween (transform, transform.position, bottomOfFlag, Time.time, flagTime);
        }
    }

    void AddMarioFlagTween() {
        float playerDistToBottomOfFlag = Vector2.Distance(player.transform.position, bottomOfFlag);
        if (marioFlagTween == null) {
            marioFlagTween = new Tween(player.transform, player.transform.position, bottomOfFlag, Time.time, (playerDistToBottomOfFlag/flagLength)*flagTime);
        }
    }

    void AddMarioTween() {
        if (marioTween == null) {
            marioTween = new Tween (player.transform, player.transform.position, endingDoor, Time.time, 2.0f);
        }
    }
}

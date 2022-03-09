using UnityEngine;

public class UITimer : UICounter
{   
    public float startTime = 400; //The time the game counts down from.
    private float remainingTime;

    private bool pauseTimer;

    protected override void Start()
    {
        base.Start();
        counterName = "TIME";
        remainingTime = startTime;
        prevValue = (int)remainingTime + 1;

        pauseTimer = false;
    }

    void Update()
    {
        if (!pauseTimer) {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                Debug.Log("Time's up!");
                globalVars.DecrementLife();
                Destroy(this);
                return;
            }
            
            if (prevValue > remainingTime)
            {
                prevValue = (int)remainingTime; //Casting rounds it down.
                UpdateUI(prevValue);
            }
        }
    }

    public void ResetTimer() {
        remainingTime = startTime + 1;
        prevValue = (int)remainingTime + 1;
    }

    public void PauseTimer (bool pause) {
        pauseTimer = pause;
    }
}

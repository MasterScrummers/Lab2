using UnityEngine;

public class UITimer : UICounter
{   
    public float startTime = 400; //The time the game counts down from.
    private float remainingTime;

    protected override void Start()
    {
        base.Start();
        counterName = "Time";
        remainingTime = startTime;
        prevValue = (int)remainingTime + 1;
    }

    void Update()
    {
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

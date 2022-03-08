using UnityEngine;

public class UILives : UICounter
{
    protected override void Start()
    {
        base.Start();
        counterName = "LIVES";
        prevValue = globalVars.lives + 1;
    }

    void Update()
    {
        CheckAndUpdateValue(globalVars.lives);
    }
}

public class UILives : UICounter
{
    protected override void Start()
    {
        base.Start();
        counterName = "Score";
        prevValue = globalVars.lives + 1;
    }

    void Update()
    {
        CheckAndUpdateValue(globalVars.lives);
    }
}

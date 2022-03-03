public class UIScore : UICounter
{
    protected override void Start()
    {
        base.Start();
        counterName = "Score";
        prevValue = globalVars.score + 1;
    }

    void Update()
    {
        CheckAndUpdateValue(globalVars.score);
    }
}

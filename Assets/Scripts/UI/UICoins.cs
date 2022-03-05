public class UICoins : UICounter
{
    protected override void Start()
    {
        base.Start();
        counterName = "COINS";
        prevValue = globalVars.coins + 1;
    }

    void Update()
    {
        CheckAndUpdateValue(globalVars.coins);
    }
}

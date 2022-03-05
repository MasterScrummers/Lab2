public class UndergroundStartup : BaseStartup
{
    protected override void Start()
    {
        base.Start();
        audioController.PlayMusic("Underground");
    }
}

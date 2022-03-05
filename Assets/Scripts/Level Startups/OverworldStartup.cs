public class OverworldStartup : BaseStartup
{
    protected override void Start()
    {
        base.Start();
        audioController.PlayMusic("Overworld");
    }
}

using UnityEngine;

public class ExtraLifeBehaviour : MovingPowerup
{
    protected override void Effect(GameObject player)
    {
        GameObject gamecontroller = DoStatic.GetGameController();
        gamecontroller.GetComponent<VariableController>().lives++;
        gamecontroller.GetComponent<AudioController>().PlaySound("1-Up");
    }
}

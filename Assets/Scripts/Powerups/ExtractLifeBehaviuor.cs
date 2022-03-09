using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractLifeBehaviuor : MovingPowerup
{
    protected override void Effect(GameObject player)
    {
        GameObject gamecontroller = DoStatic.GetGameController();
        gamecontroller.GetComponent<VariableController>().lives++;
        gamecontroller.GetComponent<AudioController>().PlaySound("1-Up");
    }


}

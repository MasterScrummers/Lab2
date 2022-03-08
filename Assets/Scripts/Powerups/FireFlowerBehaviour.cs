using UnityEngine;

public class FireFlowerBehaviour : StationaryPowerUp
{
    protected override void Effect(GameObject player)
    {
        player.GetComponent<MarioSpriteUpdator>().ChangePowerState(2);
    }
}

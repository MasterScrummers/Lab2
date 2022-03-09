using UnityEngine;

public class FireFlowerBehaviour : StationaryPowerUp
{
    protected override void Effect(GameObject player)
    {
        player.GetComponent<MarioSpriteUpdator>().setPowerState(2);
    }
}

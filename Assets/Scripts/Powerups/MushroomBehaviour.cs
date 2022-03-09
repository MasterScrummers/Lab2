using UnityEngine;

public class MushroomBehaviour : MovingPowerup
{
    protected override void Effect(GameObject player)
    {
        player.GetComponent<MarioSpriteUpdator>().setPowerState(1);
    }
}

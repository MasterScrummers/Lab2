using UnityEngine;

public class MushroomBehaviour : MovingPowerup
{
    protected override void Effect(GameObject player)
    {
        player.GetComponent<MarioSpriteUpdator>().ChangePowerState(1);
    }
}

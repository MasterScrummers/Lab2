using UnityEngine;

public class BreakBrick : BlockBase
{
    private MarioSpriteUpdator mario;

    protected override void Start()
    {
        base.Start();
        mario = DoStatic.GetPlayer().GetComponent<MarioSpriteUpdator>();
    }

    protected override void Effect(Vector3Int tilePos)
    {
        if (mario.PowerState != 0)
        {
            tilemap.SetTile(tilePos, null);
            audioController.PlaySound("Brick Break");
        }
    }
}

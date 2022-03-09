using UnityEngine;

public class ItemBlock : BlockBase
{
    public GameObject mushroom;
    public GameObject fireflower;
    private MarioSpriteUpdator mario;

    protected override void Start()
    {
        base.Start();
        mario = DoStatic.GetPlayer().GetComponent<MarioSpriteUpdator>();
    }

    protected override void Effect(Vector3Int tilePos)
    {
        if (UpdateTileBlock(tilePos))
        {
            tilePos.y++;
            GameObject item = mario.PowerState == 0 ? mushroom : fireflower;
            Instantiate(item, tilemap.GetCellCenterWorld(tilePos), Quaternion.identity);
            audioController.PlaySound("Powerup Appears");
        }
    }
}

using UnityEngine;

public class ItemBlock : BlockBase
{
    public GameObject item;

    protected override void Effect(Vector3Int tilePos)
    {
        if (UpdateTileBlock(tilePos))
        {
            Instantiate(item, tilemap.CellToWorld(tilePos), Quaternion.identity);
            audioController.PlaySound("Powerup Appears");
        }
    }
}

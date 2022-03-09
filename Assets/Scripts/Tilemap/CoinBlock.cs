using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class CoinBlock : BlockBase
{
    protected override void Effect(Vector3Int tilePos)
    {
        if (UpdateTileBlock(tilePos))
        {
            varController.coins++;
            audioController.PlaySound("Coin");
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class CoinBlock : BlockBase
{
    protected override void Effect()
    {
        if (UpdateTileBlock(tilePos))
        {
            varController.coins++;
            audioController.PlaySound("Coin");
        }
    }
}

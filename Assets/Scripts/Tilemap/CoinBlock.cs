using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class CoinBlock : BlockBase
{
    public GameObject CoinSpinEffect;

    protected override void Effect(Vector3Int tilePos)
    {
        if (UpdateTileBlock(tilePos))
        {
            varController.coins++;
            audioController.PlaySound("Coin");
            tilePos.y++;
            Instantiate(CoinSpinEffect, tilemap.GetCellCenterWorld(tilePos), Quaternion.identity);
        }
    }
}

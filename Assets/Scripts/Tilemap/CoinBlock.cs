using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class CoinBlock : BlockBase
{
    protected override void Effect()
    {
        varController.coins++;
        audioController.PlaySound("Coin");
    }
}

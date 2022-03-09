using UnityEngine;

public class CoinBlock : BlockBase
{
    protected override void Effect(Vector3Int tilePos)
    {
        varController.coins++;
        audioController.PlaySound("Coin");
    }
}

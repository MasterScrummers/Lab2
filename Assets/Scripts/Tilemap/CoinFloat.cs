using UnityEngine;

public class CoinFloat : BlockBase
{
    protected override void OnCollisionEnter2D(Collision2D col) {}

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Vector3Int cellPosition = tilemap.WorldToCell(col.transform.position);
            if (!tilemap.HasTile(cellPosition))
            {
                return;
            }

            Effect(cellPosition);
        }
    }

    protected override void Effect(Vector3Int tilePos)
    {
        tilemap.SetTile(tilePos, null);
        varController.coins++;
        audioController.PlaySound("Coin");
    }
}

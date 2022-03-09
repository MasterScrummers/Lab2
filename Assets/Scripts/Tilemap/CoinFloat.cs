using UnityEngine;

public class CoinFloat : BlockBase
{
    protected override void OnCollisionEnter2D(Collision2D col) {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Vector3Int tilePos = tilemap.WorldToCell(collision.transform.position);
        tilemap.SetTile(tilePos, null);
        varController.coins++;
        audioController.PlaySound("Coin");
    }
}

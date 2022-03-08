using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class CoinBlock : MonoBehaviour
{
    private Tilemap tilemap;

    public Tile[] blockSprites;
    private VariableController varController;
    private AudioController audioController;
    void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();
        GameObject gameController = DoStatic.GetGameController();
        varController = gameController.GetComponent<VariableController>();
        audioController = gameController.GetComponent<AudioController>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //To check whether collisions occurs with Mario's head (i.e only trigger coin from underneath block)
        if (col.collider.tag == "head")
        {
            Vector3Int cellPosition = tilemap.WorldToCell(col.GetContact(0).point);
            cellPosition.y++;

            if (!tilemap.HasTile(cellPosition))
            {
                return;
            }

            Tile colBlock = (Tile)tilemap.GetTile(cellPosition);
            if (colBlock != blockSprites[0])
            {
                varController.coins++;
                tilemap.SetTile(cellPosition, blockSprites[0]);
                audioController.PlaySound("Coin");
            }
        }
    }


}

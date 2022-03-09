using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public abstract class BlockBase : MonoBehaviour
{
    protected Tilemap tilemap;

    public Tile usedBlockSprite;
    protected VariableController varController;
    protected AudioController audioController;

    protected virtual void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();
        GameObject gameController = DoStatic.GetGameController();
        varController = gameController.GetComponent<VariableController>();
        audioController = gameController.GetComponent<AudioController>();
    }

    /// <summary>
    /// Meant to be overridden.
    /// </summary>
    protected virtual void Effect() {}

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

            Effect(cellPosition);
        }
    }

    protected bool UpdateTileBlock(Vector3Int tilePos)
    {
        if ((Tile)tilemap.GetTile(tilePos) != usedBlockSprite)
        {
            tilemap.SetTile(tilePos, usedBlockSprite);
            return true;
        }
        return false;
    }
}

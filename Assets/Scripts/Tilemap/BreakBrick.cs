using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BreakBrick : MonoBehaviour
{
    private Tilemap tilemap;
    private MarioSpriteUpdator mario;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();
        mario = DoStatic.GetPlayer().GetComponent<MarioSpriteUpdator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //To check whether collisions occurs with Mario's head (i.e only trigger coin from underneath block)
        if (col.collider.tag == "head")
        {
            if (mario.PowerState == 0)
            {
                return;
            }
            //ADD IF STATEMENT TO CHECK WHETHER PLAYER IS BIG OR SMALL MARIO
            Vector3Int cellPosition = tilemap.WorldToCell(col.GetContact(0).collider.transform.position);
            cellPosition.y++;
            tilemap.SetTile(cellPosition, null);
        }
    }
}

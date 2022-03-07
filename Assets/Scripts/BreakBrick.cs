using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakBrick : MonoBehaviour
{
    Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //To check whether collisions occurs with Mario's head (i.e only trigger coin from underneath block)
        if (col.GetContact(0).collider.tag == "head")
        {
            if (tilemap)
            {
                //ADD IF STATEMENT TO CHECK WHETHER PLAYER IS BIG OR SMALL MARIO
                Vector3Int cellPosition = tilemap.WorldToCell(col.GetContact(0).collider.transform.position);
                cellPosition.y++;
                tilemap.SetTile(cellPosition, null);
            }

        }
    }
}

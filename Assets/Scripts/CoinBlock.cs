using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CoinBlock : MonoBehaviour
{
    // Start is called before the first frame update
    int numCoins = 0;
    Tilemap tilemap;

    public Sprite[] blockSprites;
    public Text coinsUI;
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
            Vector3Int cellPosition = tilemap.WorldToCell(col.GetContact(0).collider.transform.position);
            cellPosition.y++;
            //Debug.Log(cellPosition);
            Tile colBlock = (Tile)tilemap.GetTile(cellPosition);
            Tile newtile = ScriptableObject.CreateInstance<Tile>();
            newtile.sprite = blockSprites[1];
            if (colBlock.sprite == blockSprites[0])
            {
                tilemap.SetTile(cellPosition, newtile);
                numCoins++;
                coinsUI.text = "COINS\n" + numCoins;
            }
           
        }
    }


}

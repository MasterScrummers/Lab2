using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CoinFloat : MonoBehaviour
{
    int numCoins = 0;
    Tilemap tilemap;

    public Text coinsUI;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = transform.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (tilemap)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(col.gameObject.transform.position);
                
                tilemap.SetTile(new Vector3Int(cellPosition.x+1, cellPosition.y, cellPosition.z), null);
                tilemap.SetTile(new Vector3Int(cellPosition.x-1, cellPosition.y, cellPosition.z), null);
                tilemap.SetTile(new Vector3Int(cellPosition.x, cellPosition.y+1, cellPosition.z), null);
                tilemap.SetTile(new Vector3Int(cellPosition.x, cellPosition.y-1, cellPosition.z), null);
                numCoins++;
                coinsUI.text = "COINS\n" + numCoins;
                
                Debug.Log(cellPosition); 
            }
            else
            {
                Debug.Log("No Tilemap");
            }
            
        }
    
    }
}

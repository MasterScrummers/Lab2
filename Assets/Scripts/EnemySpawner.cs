using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject goombaPrefab;

    [SerializeField] GameObject[] initialSpawn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawn in initialSpawn) {
            Instantiate(goombaPrefab, spawn.transform.position, Quaternion.identity);
            Destroy(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {      

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("GoombaSpawnPoint")) {
            Instantiate(goombaPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}

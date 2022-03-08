using UnityEngine;

public abstract class StationaryPowerUp : PowerupBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Effect(collision.gameObject);
                Destroy(gameObject);
                return;
        }
    }
}

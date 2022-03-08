using UnityEngine;

public abstract class MovingPowerup : PowerupBase
{
    public int direction = -1;
    public float speed = 6;

    protected virtual void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * speed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "terrain":
                direction *= -1;
                return;
            case "Player":
                Effect(collision.gameObject);
                Destroy(gameObject);
                return;
        }
    }
}

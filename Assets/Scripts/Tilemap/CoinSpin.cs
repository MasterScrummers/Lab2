using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float yRiseSpeed = 1;
    public float lifetime = 1.5f;

    void Update()
    {
        float delta = Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y += yRiseSpeed * delta;
        transform.position = pos;

        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}

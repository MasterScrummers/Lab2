using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Transform mario;

    void Start()
    {
        mario = DoStatic.GetPlayer().transform;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < mario.position.x)
        {
            pos.x = mario.position.x;
            transform.position = pos;
        }
    }
}

using UnityEngine;

public class ControllerFinder : MonoBehaviour
{
    public GameObject gameController { get; private set; }

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
}

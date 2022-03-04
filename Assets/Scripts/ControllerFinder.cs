using UnityEngine;

public class ControllerFinder : MonoBehaviour
{
    public GameObject gameController { get; private set; }

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
}

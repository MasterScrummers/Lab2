using UnityEngine;

/// <summary>
/// Attach this script as a component to prevent the gameobject from being
/// destroyed when changing scenes.
/// </summary>
public class Indestructable : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
}

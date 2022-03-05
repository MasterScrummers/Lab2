using UnityEngine;

[RequireComponent(typeof(ControllerFinder))]
public abstract class BaseStartup : MonoBehaviour
{
    protected AudioController audioController;

    protected virtual void Start()
    {
        audioController = GetComponent<ControllerFinder>().gameController.GetComponent<AudioController>();

    }
}

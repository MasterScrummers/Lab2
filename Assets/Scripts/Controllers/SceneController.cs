using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private string currentScene;
    private AudioController audioController;
    private Camera cam;
    private Transform player;

    void Start()
    {
        currentScene = DoStatic.GetSceneName();
        audioController = GetComponent<AudioController>();
        cam = GetComponentInChildren<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        DoSceneStartUp();
    }

    private void DoSceneStartUp()
    {
        switch(currentScene)
        {
            case "Overworld":
                audioController.PlayMusic("Overworld");
                cam.backgroundColor = DoStatic.RGBConverter(92, 148, 252);
                return;

            case "Underground":
                audioController.PlayMusic("Underground");
                cam.backgroundColor = Color.black;
                return;
        }
    }

    public void ChangeScene(string sceneName, Vector3 setPlayerPosition)
    {
        StartCoroutine(LoadLevel(sceneName, setPlayerPosition));
    }

    IEnumerator LoadLevel(string sceneName, Vector3 setPlayerPosition)
    {
        AsyncOperation async = DoStatic.LoadScene(sceneName);
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        currentScene = DoStatic.GetSceneName();
        player.position = setPlayerPosition;
        DoSceneStartUp();
    }
}

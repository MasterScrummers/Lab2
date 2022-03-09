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

    public void ChangeScene(string sceneName, Vector3 setPlayerPosition, Vector3 setCamPos)
    {
        StartCoroutine(LoadLevel(sceneName, setPlayerPosition, setCamPos));
    }

    IEnumerator LoadLevel(string sceneName, Vector3 setPlayerPosition, Vector3 setCamPos)
    {
        AsyncOperation async = DoStatic.LoadScene(sceneName);
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        currentScene = DoStatic.GetSceneName();

        player.position = setPlayerPosition;
        // setPlayerPosition.z = -10;
        GetComponentInChildren<Camera>().gameObject.transform.localPosition = setCamPos;
        DoSceneStartUp();
    }
}

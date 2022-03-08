using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoStatic
{
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static AsyncOperation LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public static Color RGBConverter(int r, int g, int b, int a = 255)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public static void LoadScene(int index)
    {
        LoadScene(SceneManager.GetSceneByBuildIndex(index).name);
    }

    public static Transform[] GetChildren(Transform transform)
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }
        return children.ToArray();
    }

    private static GameObject GetObject(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag);
    }

    public static GameObject GetGameController()
    {
        return GetObject("GameController");
    }
    public static GameObject GetPlayer()
    {
        return GetObject("Player");
    }
}

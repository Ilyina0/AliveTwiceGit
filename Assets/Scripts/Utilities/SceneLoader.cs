using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(index);
    }

    public static void LoadNewScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index >= SceneManager.sceneCount)
        {
            //处理最后一个场景
            return;
        }
        SceneManager.LoadSceneAsync(index);
    }

    public static void LoadNewScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

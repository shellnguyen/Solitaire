using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void LoadSceneWithCallback(int sceneIndex, LoadSceneMode mode, Action<AsyncOperation> callback)
    {
        StartCoroutine(LoadSceneAsyncWithCallback(sceneIndex, mode, callback));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        //Load LoadingScene first
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }

        //Then load what ever scene we need
        var request = SceneManager.LoadSceneAsync(sceneIndex);

        while(!request.isDone)
        {
            Utilities.Instance.DispatchEvent(Solitaire.Event.OnLoadingUpdated, "progress", Mathf.Clamp01(request.progress / 0.9f));

            yield return null;
        }

        yield break;
    }

    private IEnumerator LoadSceneAsyncWithCallback(int sceneIndex, LoadSceneMode mode, Action<AsyncOperation> callback)
    {
        //Then load what ever scene we need
        var request = SceneManager.LoadSceneAsync(sceneIndex, mode);
        request.completed += callback;

        while (!request.isDone)
        {
            

            yield return null;
        }

        yield break;
    }
}

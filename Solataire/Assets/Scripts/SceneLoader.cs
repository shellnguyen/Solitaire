using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
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
}

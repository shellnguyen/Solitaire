using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private EventManager m_EventManager;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadSceneAsync(1));
    }

    // Update is called once per frame
    private void Update()
    {
        
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
            DispatchEvent(Solitaire.Event.OnLoadingUpdated, "progress", Mathf.Clamp01(request.progress / 0.9f));

            yield return null;
        }

        yield break;
    }

    private void DispatchEvent<T>(Solitaire.Event eventId, string uiTag, T data)
    {
        EventParam param = new EventParam();
        param.EventID = (int)eventId;
        param.Add<string>("uiTag", uiTag);
        param.Add<T>(uiTag, data);
        m_EventManager.RaiseEvent(eventId, param);
    }
}

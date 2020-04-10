using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private int m_PrevScene = -1;

    // Start is called before the first frame update
    private void Start()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                {
                    if(m_PrevScene < 0)
                    {
                        StartCoroutine(LoadSceneAsync(1));
                    }
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        var request = SceneManager.LoadSceneAsync(sceneIndex);
        request.allowSceneActivation = false;

        while(!request.isDone)
        {
            DispatchEvent(Solitaire.Event.OnLoadingUpdated, "progress", Mathf.Clamp01(request.progress / 0.9f));

            yield return new WaitForSeconds(0.1f);
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

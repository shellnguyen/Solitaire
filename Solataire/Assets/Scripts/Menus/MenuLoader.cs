using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    private void Awake()
    {
        if(!SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            SceneLoader.Instance.LoadSceneWithCallback(SceneManager.sceneCountInBuildSettings - 1, LoadSceneMode.Additive, OnLoadUICompleted);
        }
    }

    private void OnLoadUICompleted(AsyncOperation request)
    {
        Utilities.Instance.DispatchEvent(Solitaire.Event.ShowPopup, "newgame", "");
    }
}

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
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }

    private void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(!SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

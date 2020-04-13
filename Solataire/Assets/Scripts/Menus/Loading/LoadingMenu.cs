using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private Image m_LoadingCircle;

    private void OnEnable()
    {
        m_EventManager.Register(Solitaire.Event.OnLoadingUpdated, OnLoadingUpdate);
    }

    private void OnDisable()
    {
        m_EventManager.Unregister(Solitaire.Event.OnLoadingUpdated, OnLoadingUpdate);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //m_Handler.AddListener(OnLoadingUpdate);
        SceneLoader.Instance.LoadScene(1);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnLoadingUpdate(EventParam param)
    {
        m_LoadingCircle.fillAmount = param.GetFloat("progress");
    }
}

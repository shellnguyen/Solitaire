using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMenu : EventListener
{
    [SerializeField] Image m_LoadingCircle;

    // Start is called before the first frame update
    private void Start()
    {
        m_Handler.AddListener(OnLoadingUpdate);
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

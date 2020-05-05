using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] protected Button m_BlackBG;
    [SerializeField] protected Canvas m_PopupCanvas;
    [SerializeField] protected bool m_IsClosable = true;

    protected virtual void OnEnable()
    {
        if(m_IsClosable)
        {
            m_BlackBG.onClick.AddListener(OnClickBackground);
        }
        m_PopupCanvas.worldCamera = Camera.main;
    }

    protected void OnClickBackground()
    {
        this.gameObject.SetActive(false);
    }
}

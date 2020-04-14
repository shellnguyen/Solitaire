using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private GameObject m_OptionMenuPrefab;

    //Menu scripts
    //

    private void OnEnable()
    {
        m_EventManager.Register(Solitaire.Event.ShowPopup, ShowPopup);
    }

    private void OnDisable()
    {
        m_EventManager.Unregister(Solitaire.Event.ShowPopup, ShowPopup);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void ShowPopup(EventParam param)
    {
        
    }
}

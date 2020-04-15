using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject m_OptionMenuPrefab;

    //Menu scripts
    //

    private void OnEnable()
    {
        EventManager.Instance.Register(Solitaire.Event.ShowPopup, ShowPopup);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.ShowPopup, ShowPopup);
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
        string tag = param.GetString("tag");

        if(!String.IsNullOrEmpty(tag))
        {
            switch(tag)
            {
                case "option":
                    {
                        Instantiate(m_OptionMenuPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    }
                case "newgame":
                    {
                        break;
                    }
            }
        }
    }
}

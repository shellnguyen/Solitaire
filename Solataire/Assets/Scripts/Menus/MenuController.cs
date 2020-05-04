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
    [SerializeField] private GameObject m_NewGamePopupPrefab;
    [SerializeField] private GameObject m_GameResultPopupPrefab;
    private Dictionary<string, GameObject> m_PopupList;

    //Menu scripts
    //

    private void Awake()
    {
        m_PopupList = new Dictionary<string, GameObject>();
    }

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
                        if(m_PopupList.ContainsKey(tag))
                        {
                            m_PopupList[tag].SetActive(true);
                        }
                        else
                        {
                            m_PopupList.Add(tag, Instantiate(m_OptionMenuPrefab, Vector3.zero, Quaternion.identity));
                        }                 
                        break;
                    }
                case "new_game":
                    {
                        if (m_PopupList.ContainsKey(tag))
                        {
                            m_PopupList[tag].SetActive(true);
                        }
                        else
                        {
                            m_PopupList.Add(tag, Instantiate(m_NewGamePopupPrefab, Vector3.zero, Quaternion.identity));
                        }
                        break;
                    }
                case "game_result":
                    {
                        if (m_PopupList.ContainsKey(tag))
                        {
                            m_PopupList[tag].SetActive(true);
                        }
                        else
                        {
                            m_PopupList.Add(tag, Instantiate(m_GameResultPopupPrefab, Vector3.zero, Quaternion.identity));
                            //m_PopupList[tag].GetComponent<>
                        }
                        break;
                    }
            }
        }
    }
}

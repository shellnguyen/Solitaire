using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Canvas m_MenuCanvas;
    [SerializeField] private GameData m_GameData;

    ////TopBar
    [SerializeField] private RawImage m_GameIcon;
    [SerializeField] private TextMeshProUGUI m_GameText;
    [SerializeField] private TextMeshProUGUI m_DifficultyText;
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private TextMeshProUGUI m_TimeText;
    [SerializeField] private TextMeshProUGUI m_MoveText;

    ////BottomBar
    [SerializeField] private Button m_BtnNew;
    [SerializeField] private Button m_BtnOption;
    [SerializeField] private Button m_BtnCards;
    [SerializeField] private Button m_BtnHint;
    [SerializeField] private Button m_BtnUndo;
    [SerializeField] private Button m_BtnExit;

    //[SerializeField] private EventManager m_EventManager;

    private void OnEnable()
    {
        m_MenuCanvas.worldCamera = Camera.main;
        //m_EventManager.Register(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Register(Solitaire.Event.OnDataChanged, OnDataChanged);
    }

    private void OnDisable()
    {
        //m_EventManager.Unregister(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Unregister(Solitaire.Event.OnDataChanged, OnDataChanged);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_GameText.text = Enum.GetName(typeof(Solitaire.GameMode), m_GameData.gameMode);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDataChanged(EventParam param)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "OnDataChanged");

        string tag = param.GetString("tag");
        if (!String.IsNullOrEmpty(tag))
        {
            switch (tag)
            {
                case "score":
                    {
                        m_ScoreText.text = param.GetString(tag);
                        break;
                    }
                case "move":
                    {
                        m_MoveText.text = param.GetString(tag);
                        break;
                    }
                case "time":
                    {
                        m_TimeText.text = param.GetString(tag);
                        break;
                    }
            }
        }
    }
}

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

    [SerializeField] private RawImage m_UndoIcon;
    [SerializeField] private TextMeshProUGUI m_UndoText;
    [SerializeField] private Color m_UndoColor;

    //[SerializeField] private EventManager m_EventManager;

    private void OnEnable()
    {
        m_MenuCanvas.worldCamera = Camera.main;
        //m_EventManager.Register(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Register(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Register(Solitaire.Event.ChangeUIStatue, OnBtnUndoChanged);

        SetupButton();
    }

    private void OnDisable()
    {
        //m_EventManager.Unregister(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Unregister(Solitaire.Event.OnDataChanged, OnDataChanged);
        EventManager.Instance.Unregister(Solitaire.Event.ChangeUIStatue, OnBtnUndoChanged);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_UndoColor = Color.white;
        //m_GameText.text = Enum.GetName(typeof(Solitaire.GameMode), m_GameData.gameMode);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SetupButton()
    {
        m_BtnNew.onClick.AddListener(OnNewGameClicked);
        m_BtnOption.onClick.AddListener(OnOptionClicked);
        m_BtnCards.onClick.AddListener(OnCardClicked);
        m_BtnUndo.onClick.AddListener(OnUndoClicked);
        m_BtnHint.onClick.AddListener(OnHintClicked);
        m_BtnExit.onClick.AddListener(OnExitClicked);
    }

    private void OnNewGameClicked()
    {
        m_MoveText.text = "0";
        m_ScoreText.text = "0";
        m_TimeText.text = "00:00";

        Utilities.Instance.DispatchEvent(Solitaire.Event.OnNewGame, "new_game", "");
    }

    private void OnOptionClicked()
    {
        Utilities.Instance.DispatchEvent(Solitaire.Event.ShowPopup, "option", "");
    }

    private void OnCardClicked()
    {
        //todo
    }

    private void OnUndoClicked()
    {
        //todo
        Utilities.Instance.DispatchEvent(Solitaire.Event.UndoMove, "undo_move", 0);
    }

    private void OnHintClicked()
    {
        //todo
        //Temporally make this button to How to Play
        Utilities.Instance.DispatchEvent(Solitaire.Event.ShowPopup, "how_to_play", 0);
    }

    private void OnExitClicked()
    {
        SceneLoader.Instance.LoadScene(1);
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
                case "difficulty":
                    {
                        m_DifficultyText.text = param.GetString(tag);
                        break;
                    }
                case "game_mode":
                    {
                        m_GameText.text = param.GetString(tag);
                        break;
                    }
            }
        }
    }

    private void OnBtnUndoChanged(EventParam param)
    {
        string tag = param.GetString("tag");
        if(tag.Equals("undo"))
        {
            m_BtnUndo.interactable = param.GetBoolean(tag);
            
            if(m_BtnUndo.IsInteractable())
            {
                m_UndoColor.a = 1.0f;
            }
            else
            {
                m_UndoColor.a = 0.5f;
            }

            m_UndoIcon.color = m_UndoColor;
            m_UndoText.color = m_UndoColor;

        }
    }
}

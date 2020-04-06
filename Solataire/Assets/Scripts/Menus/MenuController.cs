using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameData m_GameData;

    //TopBar
    [SerializeField] private RawImage m_GameIcon;
    [SerializeField] private TextMesh m_GameText;
    [SerializeField] private TextMesh m_DifficultyText;
    [SerializeField] private TextMesh m_ScoreText;
    [SerializeField] private TextMesh m_TimeText;
    [SerializeField] private TextMesh m_MoveText;

    //BottomBar
    [SerializeField] private Button m_BtnNew;
    [SerializeField] private Button m_BtnOption;
    [SerializeField] private Button m_BtnCards;
    [SerializeField] private Button m_BtnHint;
    [SerializeField] private Button m_BtnUndo;
    [SerializeField] private Button m_BtnExit;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

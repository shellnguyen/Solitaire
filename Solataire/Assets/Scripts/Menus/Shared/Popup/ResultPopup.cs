using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : Popup
{
    [SerializeField] private TextMeshProUGUI m_ResultText;
    [SerializeField] private Button m_BtnPlayAgain;
    [SerializeField] private Button m_BtnQuit;
    [SerializeField] private bool m_IsWin;

    public bool IsWin
    {
        get
        {
            return m_IsWin;
        }

        set
        {
            m_IsWin = value;
            SetResultText();
        }
    }

    private void Start()
    {
        m_BtnPlayAgain.onClick.AddListener(delegate { OnButtonClicked(1); });
        m_BtnQuit.onClick.AddListener(delegate { OnButtonClicked(2); });
    }

    private void SetResultText()
    {
        if(m_IsWin)
        {
            m_ResultText.text = "Congratulation! You Win!";
        }
        else
        {
            m_ResultText.text = "Out of moves! You Lose!";
        }
        
    }

    private void OnButtonClicked(int buttonId)
    {
        switch(buttonId)
        {
            case 1:
                {
                    this.gameObject.SetActive(false);
                    Utilities.Instance.DispatchEvent(Solitaire.Event.OnNewGame, "new_game", "");
                    break;
                }
            case 2:
                {
                    this.gameObject.SetActive(false);
                    SceneLoader.Instance.LoadScene(1); // Return to main menu
                    break;
                }
        }
    }
}

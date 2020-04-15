using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PlayerName;
    [SerializeField] private TextMeshProUGUI m_LeaderboardName;
    [SerializeField] private TextMeshProUGUI m_LeaderboardRank;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnButtonPressed(int buttonId)
    {
        switch(buttonId)
        {
            case 0: //Exit
                {
                    break;
                }
            case 10: //Option
                {
                    break;
                }
            case 11: //Challenger
                {
                    break;
                }
            case 12: //Leaderboard
                {
                    break;
                }
            case 13: //Option
                {
                    Utilities.Instance.DispatchEvent(Solitaire.Event.ShowPopup, "option", "");
                    break;
                }
            case 14: //Exit
                {
                    Application.Quit();
                    break;
                }
            case 15: //Login
                {
                    break;
                }
            default: //GameButton
                {
                    SceneLoader.Instance.LoadScene(buttonId);
                    break;
                }
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Text
    [SerializeField] private TextMeshProUGUI m_GreetingText;
    [SerializeField] private TextMeshProUGUI m_PlayerNameText;

    //Button
    [SerializeField] private Button m_BtnKlondike;
    [SerializeField] private Button m_BtnSpider;
    [SerializeField] private Button m_BtnPyramic;
    [SerializeField] private Button m_BtnFreeCall;
    [SerializeField] private Button m_BtnTriPeak;

    [SerializeField] private Button m_BtnLogin;
    [SerializeField] private Button m_BtnSetting;
    [SerializeField] private Button m_BtnChallenge;
    [SerializeField] private Button m_BtnLeaderboard;

    //Main menu Ratio
    [SerializeField] private AspectRatioFitter m_AspectFitter;

    //Ads
    [SerializeField] private GameObject m_AdsPanel;

    private void OnEnable()
    {
        //EventManager.Instance.Register(Solitaire.Event.PostAdsInitialized, TriggerAds);
    }

    private void OnDisable()
    {
        //EventManager.Instance.Unregister(Solitaire.Event.PostAdsInitialized, TriggerAds);
    }

    private void Awake()
    {
        m_AspectFitter.aspectRatio = (float)Screen.width / (float)Screen.height;
        m_AdsPanel.SetActive(GameSetting.Instance.enableAds);

        m_BtnKlondike.onClick.AddListener(delegate { OnButtonPressed((int)Solitaire.GameMode.Klondike); });
        m_BtnSpider.onClick.AddListener(delegate { OnButtonPressed((int)Solitaire.GameMode.Spider); });
        m_BtnPyramic.onClick.AddListener(delegate { OnButtonPressed((int)Solitaire.GameMode.Pyramid); });
        m_BtnTriPeak.onClick.AddListener(delegate { OnButtonPressed((int)Solitaire.GameMode.TriPeak); });
        m_BtnFreeCall.onClick.AddListener(delegate { OnButtonPressed((int)Solitaire.GameMode.FreeCall); });

        m_BtnLogin.onClick.AddListener(delegate { OnButtonPressed(14); });
        m_BtnSetting.onClick.AddListener(delegate { OnButtonPressed(13); });
        m_BtnLeaderboard.onClick.AddListener(delegate { OnButtonPressed(12); });
        m_BtnChallenge.onClick.AddListener(delegate { OnButtonPressed(11); });

        //Utilities.Instance.DispatchEvent(Solitaire.Event.LoadData, "load_data", 0);
        //AdsController.Instance.Initialized();
        AppController.Instance.LoadSetting();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //private void TriggerAds(EventParam param)
    //{
    //    AdsController.Instance.ShowBanner();
    //}

    public void OnButtonPressed(int buttonId)
    {
        switch(buttonId)
        {
            case 0: //Exit
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
            case 14: //Login
                {
                    break;
                }
            default: //GameButton
                {
                    if(GameSetting.Instance.enableAds)
                    {
                        AdsController.Instance.HideBanner();
                    }
                    SceneLoader.Instance.LoadScene(buttonId);
                    break;
                }
        }
    }
}

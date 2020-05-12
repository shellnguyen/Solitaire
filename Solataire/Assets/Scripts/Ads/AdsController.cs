using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsController : Singleton<AdsController>
{
    private AbmobController m_AdmobController;
    private UnityAdsController m_UnityAdsController;
    private bool m_IsBannerShow;
    private bool m_IsVideoShowing;

    public bool IsVideoShowing
    {
        get
        {
            return m_IsVideoShowing;
        }
    }

    public bool IsBannerShow
    {
        get
        {
            return m_IsBannerShow;
        }
    }

    private void Awake()
    {
        m_IsBannerShow = false;
        m_IsVideoShowing = false;
    }

    private void Start()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController OnStart");
#if UNITY_EDITOR || DEV_BUILD
        m_AdmobController = new AbmobController(MasterData.Instance.Admob_AdUnit_Banner_Test_Id);
        m_UnityAdsController = new UnityAdsController(MasterData.Instance.UnityAds_GameId, true, MasterData.Instance.UnityAds_Interstitial_Id);
#else
        m_AdmobController = new AbmobController(this, MasterData.Instance.Admob_AdUnit_Banner_Id);
        m_UnityAdsController = new UnityAdsController(MasterData.Instance.UnityAds_GameId, false, MasterData.Instance.UnityAds_Banner_Id, MasterData.Instance.UnityAds_Interstitial_Id);
#endif

        m_AdmobController.Initialize();
        m_UnityAdsController.Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ShowBanner()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController ShowBanner");
        m_IsBannerShow = true;
        m_AdmobController.ShowBanner();
    }

    public void ShowFullScreen()
    {
        m_UnityAdsController.ShowIncentivized();
    }

    public void OnAdInit(Solitaire.NetworkType type)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController OnAdInit " + type.ToString());
    }

    public void OnAdLoaded(Solitaire.NetworkType type)
    {
        if ((type == Solitaire.NetworkType.Admob) && (SceneManager.GetActiveScene().buildIndex == 1))
        {
            UnityMainThreadDispatcher.Instance().Enqueue(DispatchEvent(type, Solitaire.AdsType.Banner));
        }
    }
    
    public void OnAdFailedToLoad()
    {

    }

    public void OnAdStarted()
    {
        m_IsVideoShowing = true;
    }

    public void OnAdFinished()
    {
        m_IsVideoShowing = false;
    }

    public void OnAdOpened()
    {
    }

    public void OnAdClosed()
    {
        m_IsVideoShowing = false;
    }

    public void OnAdFailedToShow()
    {
        m_IsVideoShowing = false;
    }

    private IEnumerator DispatchEvent(Solitaire.NetworkType network, Solitaire.AdsType adsType)
    {
        if(!m_IsBannerShow)
        Utilities.Instance.DispatchEvent(Solitaire.Event.PostAdsInitialized, network.ToString(), 1);
        yield return null;
    }
}

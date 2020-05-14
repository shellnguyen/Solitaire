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

    public void Initialized()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController Initialized");

        m_IsBannerShow = false;
        m_IsVideoShowing = false;
#if UNITY_EDITOR || DEV_BUILD
        m_AdmobController = new AbmobController(this, MasterData.Instance.Admob_AdUnit_Banner_Test_Id);
        m_UnityAdsController = new UnityAdsController(this, MasterData.Instance.UnityAds_GameId, true, MasterData.Instance.UnityAds_Interstitial_Id);
#else
        m_AdmobController = new AbmobController(this, MasterData.Instance.Admob_AdUnit_Banner_Id);
        m_UnityAdsController = new UnityAdsController(MasterData.Instance.UnityAds_GameId, false, MasterData.Instance.UnityAds_Banner_Id, MasterData.Instance.UnityAds_Interstitial_Id);
#endif

        m_AdmobController.Initialize();
        m_UnityAdsController.Initialize();
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

    public void HideBanner()
    {
        m_AdmobController.HideBanner();
        m_IsBannerShow = false;
    }

    //Callback when Init
    public void OnAdInit(Solitaire.NetworkType type)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController OnAdInit " + type.ToString());
    }

    //Callback when successfully loaded ads
    public void OnAdLoaded(Solitaire.NetworkType type)
    {
        if ((type == Solitaire.NetworkType.Admob))
        {
            UnityMainThreadDispatcher.Instance().Enqueue(ShowBannerWhenReady());
        }
    }
    
    //Callback when failed to loaded ads
    public void OnAdFailedToLoad()
    {

    }

    //Callback when successfully triggered ads
    public void OnAdStarted()
    {
        m_IsVideoShowing = true;
    }

    //Callback when finished ads
    public void OnAdFinished()
    {
        m_IsVideoShowing = false;
    }

    //Callback when clicked/open external link in ads
    public void OnAdOpened()
    {
    }

    //Callback when closed ads before finishing
    public void OnAdClosed()
    {
        m_IsVideoShowing = false;
    }

    //Callback when failed to triggered ads
    public void OnAdFailedToShow()
    {
        m_IsVideoShowing = false;
    }

    private IEnumerator ShowBannerWhenReady()
    {
        if(!m_IsBannerShow && (SceneManager.GetActiveScene().buildIndex == 1) && GameSetting.Instance.enableAds)
        {
            ShowBanner();
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsController : IAdNetworkController, IUnityAdsListener
{
    private AdsController m_Controller;
    private readonly string m_GameId;
    private readonly string m_VideoPlacement;
    private bool m_TestMode;

    public UnityAdsController(AdsController controller, string gameId, bool testMode, string videoId)
    {
        m_Controller = controller;
        m_GameId = gameId;
        m_TestMode = testMode;
        m_VideoPlacement = videoId;
    }

    public void Initialize()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(m_GameId, m_TestMode);
    }

    #region UnityAds callbacks
    public void OnUnityAdsDidError(string message)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Error !!! Message = " + message);
       m_Controller.OnAdFailedToLoad();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Finished !!!");
        if(showResult == ShowResult.Skipped)
        {
           m_Controller.OnAdClosed();
        }
        else
        {
            if(showResult == ShowResult.Finished)
            {
               m_Controller.OnAdFinished();
            }
            else
            {
               m_Controller.OnAdFailedToShow();
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Started !!!");
        m_Controller.OnAdStarted();
    }

    public void OnUnityAdsReady(string placementId)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Ready !!!");
        //if (m_VideoPlacement.Equals(placementId))
        //{
        //    Advertisement.Show(m_VideoPlacement);
        //}
    }
    #endregion

    public void ShowBanner(Solitaire.BannerPosition position = Solitaire.BannerPosition.Bottom)
    {
        //Advertisement.Banner.Show(m_BannerPlacement);
    }

    public void ShowIncentivized()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show(m_VideoPlacement);
        }
    }

    public void ShowInterstitial()
    {
        //
    }

    public void HideBanner()
    {
    }
}

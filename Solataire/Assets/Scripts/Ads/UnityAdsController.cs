using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsController : IAdNetworkController, IUnityAdsListener
{
    private readonly string m_GameId;
    private readonly string m_VideoPlacement;
    private bool m_TestMode;

    public UnityAdsController(string gameId, bool testMode, string videoId)
    {
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
        AdsController.Instance.OnAdFailedToLoad();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Finished !!!");
        if(showResult == ShowResult.Skipped)
        {
            AdsController.Instance.OnAdClosed();
        }
        else
        {
            if(showResult == ShowResult.Finished)
            {
                AdsController.Instance.OnAdFinished();
            }
            else
            {
                AdsController.Instance.OnAdFailedToShow();
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "UnitAds Video Started !!!");
        AdsController.Instance.OnAdStarted();
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
}

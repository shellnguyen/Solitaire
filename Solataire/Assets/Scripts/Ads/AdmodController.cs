using UnityEngine;
using GoogleMobileAds.Api;
using System;
using Solitaire;

public class AbmobController : IAdNetworkController
{
    private AdsController m_Controller;
    private BannerView m_BannerView;
    private bool m_IsInitialized;

    private string m_BannerUnitId;

    public bool IsInitialized
    {
        get
        {
            return m_IsInitialized;
        }
    }

    public AbmobController(AdsController controller, string bannerUnitId)
    {
        m_Controller = controller;
        m_IsInitialized = false;
        m_BannerUnitId = bannerUnitId;
    }

    public void Initialize()
    {
        MobileAds.Initialize(OnAdsInitialzation);
    }

    public void ShowBanner(BannerPosition position = BannerPosition.Bottom)
    {
        switch(position)
        {
            case Solitaire.BannerPosition.BottomLeft:
                {
                    m_BannerView.SetPosition(AdPosition.BottomLeft);
                    break;
                }
            case Solitaire.BannerPosition.BottomRight:
                {
                    m_BannerView.SetPosition(AdPosition.BottomRight);
                    break;
                }
            case Solitaire.BannerPosition.Top:
                {
                    m_BannerView.SetPosition(AdPosition.Top);
                    break;
                }
            case Solitaire.BannerPosition.TopLeft:
                {
                    m_BannerView.SetPosition(AdPosition.TopLeft);
                    break;
                }
            case Solitaire.BannerPosition.TopRight:
                {
                    m_BannerView.SetPosition(AdPosition.TopRight);
                    break;
                }
            default:
                {
                    m_BannerView.SetPosition(AdPosition.Bottom);
                    break;
                }
        }
        m_BannerView.Show();
    }

    public void ShowInterstitial()
    {
        //m_BannerView.
    }

    public void ShowIncentivized()
    {
    }

    public void HideBanner()
    {
        if(m_Controller.IsBannerShow)
        {
            m_BannerView.Hide();
        }
    }

    private void RequestBanner()
    {
        m_BannerView = new BannerView(m_BannerUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.m_BannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.m_BannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.m_BannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.m_BannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.m_BannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.m_BannerView.LoadAd(request);
        //this.m_BannerView.Show();
        m_IsInitialized = true;
    }

    private void OnAdsInitialzation(InitializationStatus status)
    {
        m_IsInitialized = true;
        RequestBanner();
       m_Controller.OnAdInit(NetworkType.Admob);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
       m_Controller.OnAdLoaded(NetworkType.Admob);
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
       m_Controller.OnAdFailedToLoad();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
       m_Controller.OnAdOpened();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
       m_Controller.OnAdClosed();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
        //AdsController.Instance.OnAdClosed();
    }
}

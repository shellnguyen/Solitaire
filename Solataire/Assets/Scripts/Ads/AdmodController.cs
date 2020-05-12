using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AbmobController
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

    public AbmobController(AdsController adsController, string bannerUnitId)
    {
        m_IsInitialized = false;
        m_Controller = adsController;
        m_BannerUnitId = bannerUnitId;
    }

    public void Initialize()
    {
        MobileAds.Initialize(OnAdsInitialzation);
    }

    public void ShowBanner()
    {
        m_BannerView.Show();
    }

    private void OnAdsInitialzation(InitializationStatus status)
    {
        RequestBanner();
        m_Controller.OnAdInit();
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

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        m_Controller.OnAdLoaded();
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
        //m_Controller.OnAdClosed();
    }
}

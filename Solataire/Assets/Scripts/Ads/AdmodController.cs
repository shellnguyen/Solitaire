using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmodController
{
    private AdsController m_Controller;
    private BannerView m_BannerView;

    public AdmodController(AdsController adsController)
    {
        m_Controller = adsController;
    }

    public void Initialize()
    {
        MobileAds.Initialize(OnAdsInitialzation);
    }

    private void OnAdsInitialzation(InitializationStatus status)
    {
        
    }

    private void RequestBanner()
    {
#if UNITY_EDITOR || DEV_BUILD
        m_BannerView = new BannerView(MasterData.Instance.Admod_AdUnit_Banner_Test_Id, AdSize.Banner, AdPosition.Bottom);
#else
        m_BannerView = new BannerView(MasterData.Instance.Admod_AdUnit_Banner_Id, AdSize.Banner, AdPosition.Bottom);
#endif

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
        this.m_BannerView.Show();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}

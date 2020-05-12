using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : Singleton<AdsController>
{
    private AbmobController m_AdmobController;

    private void Start()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController OnStart");
#if UNITY_EDITOR || DEV_BUILD
        m_AdmobController = new AbmobController(this, MasterData.Instance.Admob_AdUnit_Banner_Test_Id);
#else
        m_AdmobController = new AbmobController(this, MasterData.Instance.Admob_AdUnit_Banner_Id);
#endif
        m_AdmobController.Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ShowBanner()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController ShowBanner");
        m_AdmobController.ShowBanner();
    }

    public void OnAdInit()
    {
        Logger.Instance.PrintLog(Common.DEBUG_TAG, "AdsController OnAdInit");
        UnityMainThreadDispatcher.Instance().Enqueue(DispatchEvent());
    }

    public void OnAdLoaded()
    {

    }
    
    public void OnAdFailedToLoad()
    {

    }

    public void OnAdOpened()
    {

    }

    public void OnAdClosed()
    {

    }

    private IEnumerator DispatchEvent()
    {
        Utilities.Instance.DispatchEvent(Solitaire.Event.PostAdsInitialized, "admob", 1);
        yield return null;
    }
}

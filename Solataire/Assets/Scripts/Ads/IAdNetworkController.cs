using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdNetworkController
{
    void Initialize();
    void ShowBanner(Solitaire.BannerPosition position = Solitaire.BannerPosition.Bottom);
    void ShowInterstitial();
    void ShowIncentivized();
}

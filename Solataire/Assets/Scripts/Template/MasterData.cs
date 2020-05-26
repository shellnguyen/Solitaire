using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MasterData", menuName ="Template/MasterData")]
public class MasterData : ScriptableSingleton<MasterData>
{
    //Admod
    public string Admob_AppId;
    public string Admob_AdUnit_Banner_Id;
    public string Admob_AdUnit_Banner_Test_Id;
    //

    //UnityAds
    public string UnityAds_GameId;
    public string UnityAds_Interstitial_Id;
    //
}

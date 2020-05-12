using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MasterData", menuName ="Template/MasterData")]
public class MasterData : ScriptableSingleton<MasterData>
{
    //Admod
    public readonly string Admob_AppId;
    public readonly string Admob_AdUnit_Banner_Id;
    public readonly string Admob_AdUnit_Banner_Test_Id;
    //

    //UnityAds
    public string UnityAds_AppId;
    //
}

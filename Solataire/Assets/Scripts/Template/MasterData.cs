using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MasterData", menuName ="Template/MasterData")]
public class MasterData : ScriptableSingleton<MasterData>
{
    //Admod
    public string Admod_AppId;
    public string Admod_AdUnit_Banner_Id;
    public string Admod_AdUnit_Banner_Test_Id;
    //

    //UnityAds
    public string UnityAds_AppId;
    //
}

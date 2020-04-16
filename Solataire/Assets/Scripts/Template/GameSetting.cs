using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameSetting", menuName ="Template/GameSetting")]
public class GameSetting : ScriptableSingleton<GameSetting>
{
    public Difficulty difficulty;
    public bool m_HasAudio;
    public bool m_EnableAds;
}

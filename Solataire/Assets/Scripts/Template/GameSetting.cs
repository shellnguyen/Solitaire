using UnityEngine;

[CreateAssetMenu(fileName ="GameSetting", menuName ="Template/GameSetting")]
public class GameSetting : ScriptableSingleton<GameSetting>
{
    public Difficulty difficulty;
    public bool enableAudio;
    public bool enableAds;
}

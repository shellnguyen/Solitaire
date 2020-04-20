using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : Popup
{
    [SerializeField] GameOption m_AudioOption;
    [SerializeField] GameOption m_AdsOption;

    // Start is called before the first frame update
    private void OnStart()
    {
        m_AudioOption.Initialized("audio", GameSetting.Instance.m_HasAudio);
        m_AdsOption.Initialized("enable_ads", GameSetting.Instance.m_EnableAds);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnSettingChanged(EventParam param)
    {
        string tag = param.GetString("tag");
        switch(tag)
        {
            case "audio":
                {
                    GameSetting.Instance.m_HasAudio = param.GetBoolean(tag);
                    break;
                }
            case "enable_ads":
                {
                    GameSetting.Instance.m_EnableAds = param.GetBoolean(tag);
                    break;
                }
        }
    }
}

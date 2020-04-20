using UnityEngine;

public class OptionPopup : Popup
{
    [SerializeField] GameOption m_AudioOption;
    [SerializeField] GameOption m_AdsOption;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.Instance.Register(Solitaire.Event.OnValueChanged, OnSettingChanged);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.OnValueChanged, OnSettingChanged);
    }

    // Start is called before the first frame update
    private void OnStart()
    {
        m_AudioOption.Initialized("audio", GameSetting.Instance.enableAudio);
        m_AdsOption.Initialized("enable_ads", GameSetting.Instance.enableAds);
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
                    GameSetting.Instance.enableAudio = param.GetBoolean(tag);
                    break;
                }
            case "enable_ads":
                {
                    GameSetting.Instance.enableAds = param.GetBoolean(tag);
                    break;
                }
        }
    }
}

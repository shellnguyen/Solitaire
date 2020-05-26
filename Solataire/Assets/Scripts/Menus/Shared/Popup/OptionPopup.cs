using UnityEngine;

public class OptionPopup : Popup
{
    [SerializeField] GameOption m_AudioOption;
    [SerializeField] GameOption m_AdsOption;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.Instance.Register(Solitaire.Event.OnValueChanged, OnSettingValueChanged);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.OnValueChanged, OnSettingValueChanged);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_AudioOption.Initialized("audio", GameSetting.Instance.enableAudio);
        m_AdsOption.Initialized("enable_ads", GameSetting.Instance.enableAds);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnSettingValueChanged(EventParam param)
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

        Utilities.Instance.DispatchEvent(Solitaire.Event.OnSettingChanged, "setting_changed", 0);
        AppController.Instance.SaveSetting();
        //Utilities.Instance.DispatchEvent(Solitaire.Event.SaveData, "save_data", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_MainSource;

    private void OnEnable()
    {
        EventManager.Instance.Register(Solitaire.Event.PlayAudio, OnPlayAudio);
        EventManager.Instance.Register(Solitaire.Event.OnSettingChanged, SetAudioVolume);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.PlayAudio, OnPlayAudio);
        EventManager.Instance.Unregister(Solitaire.Event.OnSettingChanged, SetAudioVolume);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_MainSource = this.GetComponent<AudioSource>();
    }

    private void OnPlayAudio(EventParam param)
    {
        string tag = param.GetString("tag");
        switch(tag)
        {
            case "play_one":
                {
                    PlayAudio(param.GetInt(tag));
                    break;
                }
            case "play_loop":
                {
                    PlayLoop(param.GetInt(tag));
                    break;
                }
        }
    }

    private void PlayAudio(int audioId)
    {
        m_MainSource.PlayOneShot(ResourcesManager.Instance.AudioList[audioId]);
    }

    private void PlayLoop(int audioId)
    {
        m_MainSource.clip = ResourcesManager.Instance.AudioList[audioId];
        m_MainSource.Play();
    }

    private void StopAudio()
    {
        m_MainSource.Stop();
    }

    private void SetAudioVolume(EventParam param)
    {
        m_MainSource.volume = GameSetting.Instance.enableAudio ? 1.0f : 0.0f;
    }
}

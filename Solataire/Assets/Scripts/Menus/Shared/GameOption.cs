using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOption : MonoBehaviour
{
    [SerializeField] private string m_OptionKey;
    [SerializeField] private bool m_IsEnable;
    [SerializeField] private ToggleController m_Toggle;

    public void Initialized(string key, bool enable)
    {
        m_OptionKey = key;
        m_IsEnable = enable;
        SetData();
    }

    private void SetData()
    {
        m_Toggle.IsOn = m_IsEnable;
    }
}

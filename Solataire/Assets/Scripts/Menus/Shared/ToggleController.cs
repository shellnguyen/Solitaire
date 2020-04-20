using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Image m_ToggleBar;
    [SerializeField] private Image m_ToggleBall;
    [SerializeField] private Button m_BtnToogle;
    [SerializeField] private Color m_OnColor;
    [SerializeField] private Color m_OffColor;
    [SerializeField] private bool m_IsOn;
    [SerializeField] private string m_Key;

    public bool IsOn
    {
        get
        {
            return m_IsOn;
        }

        set
        {
            m_IsOn = value;
            OnStateChanged();
        }
    }

    public string Key
    {
        get
        {
            return m_Key;
        }

        set
        {
            m_Key = value;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_BtnToogle.onClick.AddListener(OnToggleClicked);
        m_ToggleBar.color = m_OffColor;
    }

    private void OnStateChanged()
    {
        if(m_IsOn)
        {
            m_ToggleBar.color = m_OnColor;
            m_ToggleBall.rectTransform.localPosition = new Vector3(m_ToggleBar.rectTransform.rect.width/2, 0.0f, 0.0f);
        }
        else
        {
            m_ToggleBar.color = m_OffColor;
            m_ToggleBall.rectTransform.localPosition = new Vector3(-m_ToggleBar.rectTransform.rect.width/2, 0.0f, 0.0f);
        }

        Utilities.Instance.DispatchEvent(Solitaire.Event.OnValueChanged, m_Key, m_IsOn);
    }

    private void OnToggleClicked()
    {
        if(m_IsOn)
        {
            IsOn = false;
        }
        else
        {
            IsOn = true;
        }
    }
}

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
            m_ToggleBall.rectTransform.localPosition = new Vector3(50.0f, 0.0f, 0.0f);
        }
        else
        {
            m_ToggleBar.color = m_OffColor;
            m_ToggleBall.rectTransform.localPosition = new Vector3(-50.0f, 0.0f, 0.0f);
        }
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

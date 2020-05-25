using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_DisplayText;
    [SerializeField] private Button m_BtnPrev;
    [SerializeField] private Button m_BtnNext;
    [SerializeField] private List<string> m_Data;
    [SerializeField] private sbyte m_Index;

    // Start is called before the first frame update
    private void Awake()
    {
        m_Index = 0;
        m_Data = new List<string>();

        m_BtnNext.onClick.AddListener(delegate { OnButtonPressed(1); });
        m_BtnPrev.onClick.AddListener(delegate { OnButtonPressed(-1); });
    }

    private void OnButtonPressed(sbyte offset)
    {
        m_Index += offset;

        m_DisplayText.text = m_Data[m_Index];

        m_BtnNext.interactable = m_Index < m_Data.Count - 1 ? true : false;
        m_BtnPrev.interactable = m_Index > 0 ? true : false;
    }

    public void SetData(List<string> data)
    {
        m_Data = data;
        m_DisplayText.text = m_Data[m_Index];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayPopup : Popup
{
    [SerializeField] private ScrollController m_ScrollPanel;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void SetData()
    {
        m_ScrollPanel.SetData(ResourcesManager.Instance.HintTexts);
    }
}

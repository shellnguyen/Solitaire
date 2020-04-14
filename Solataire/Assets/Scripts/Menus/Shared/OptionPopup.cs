using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    [SerializeField] private Button m_BlackBG;
    [SerializeField] private Canvas m_OptionCanvas;

    private void OnEnable()
    {
        m_BlackBG.onClick.AddListener(OnClickBackground);
        m_OptionCanvas.worldCamera = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnClickBackground()
    {
        this.gameObject.SetActive(false);
    }
}

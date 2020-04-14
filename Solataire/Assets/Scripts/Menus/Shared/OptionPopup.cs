using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    [SerializeField] private Button m_BlackBG;

    // Start is called before the first frame update
    private void Start()
    {
        m_BlackBG.onClick.AddListener(OnClickBackground);
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

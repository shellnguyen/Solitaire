using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSkin : MonoBehaviour
{
    [SerializeField] private Image m_SourceImage;
    [SerializeField] private Button m_AttachedBtn;
    [SerializeField] private CardSkinPopup m_CardSkinPopup;
    [SerializeField] private int m_Index;

    public int Index
    {
        get
        {
            return m_Index;
        }

        set
        {
            m_Index = value;
        }
    }

    public Image SourceImage
    {
        get
        {
            return m_SourceImage;
        }
    }

    public CardSkinPopup CardSkinPopup
    {
        set
        {
            m_CardSkinPopup = value;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        m_SourceImage = GetComponent<Image>();
        m_AttachedBtn = GetComponent<Button>();
    }

    private void Start()
    {
        m_AttachedBtn.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        m_CardSkinPopup.OnCardSelected(m_Index);
    }
}

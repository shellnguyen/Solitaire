using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSkinPopup : Popup
{
    [SerializeField] private GameObject m_CardSkinPrefab;
    [SerializeField] private GameObject m_ScrollViewContent;

    //ConfirmPopup
    [SerializeField] private GameObject m_ConfirmPopup;
    
    [SerializeField] private Image m_FrontCard;
    [SerializeField] private Image m_LeftCard;
    [SerializeField] private Image m_RightCard;

    [SerializeField] private Button m_BtnApply;
    [SerializeField] private Button m_BtnCancel;

    // Start is called before the first frame update
    private void Start()
    {
        PopulateCardSkins();
        m_BtnApply.onClick.AddListener(OnApplySkin);
        m_BtnCancel.onClick.AddListener(OnCancel);
    }

    private void PopulateCardSkins()
    {
        GameObject temp;
        for(int i = 0; i < ResourcesManager.Instance.CardSprite.Count; ++i)
        {
            temp = Instantiate(m_CardSkinPrefab, m_ScrollViewContent.transform, false);
            CardSkin cardSkin = temp.GetComponent<CardSkin>();
            cardSkin.SourceImage.sprite = ResourcesManager.Instance.CardSprite[i];
            cardSkin.Index = i;
            cardSkin.CardSkinPopup = this;

            if(i == 0)
            {
                cardSkin.SourceImage.color = Color.yellow;
            }
        }
    }

    private void OnApplySkin()
    {

    }

    private void OnCancel()
    {
        m_ConfirmPopup.SetActive(false);
    }

    public void OnCardSelected(int index)
    {
        Sprite texCard = ResourcesManager.Instance.CardSprite[index];
        m_FrontCard.sprite = texCard;
        m_LeftCard.sprite = texCard;
        m_RightCard.sprite = texCard;

        m_ConfirmPopup.SetActive(true);
    }
}

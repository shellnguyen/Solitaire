using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour
{
    [SerializeField]private GameData m_GameData;
    [SerializeField]private GameController m_GameController;
    [SerializeField]private SpriteRenderer m_Renderer;
    [SerializeField]private Sprite m_Back;
    [SerializeField]private Sprite m_Empty;
    [SerializeField]private sbyte m_CurrentDrawCard;
    [SerializeField]private bool m_IsEmpty;

    private void OnEnable()
    {
        EventManager.Instance.Register(Solitaire.Event.OnDataChanged, OnUndoPutback);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.OnDataChanged, OnUndoPutback);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_GameData.currentDrawCard = -1;
        m_IsEmpty = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!m_IsEmpty)
        {
            m_GameData.currentDrawCard++;
            m_GameController.DrawCardFromDeck();

            if(m_GameData.currentDrawCard >= m_GameData.deckCards.Count - 1)
            {
                m_IsEmpty = true;
                m_Renderer.sprite = m_Empty;
            }
        }
        else
        {
            m_GameData.currentDrawCard = - 1;
            m_IsEmpty = false;
            m_GameController.PutBackToDeck();
            m_Renderer.sprite = m_Back;
        }
    }

    private void OnUndoPutback(EventParam param)
    {
        string tag = param.GetString("tag");
        if(tag.Equals("undo_putback"))
        {
            m_IsEmpty = param.GetBoolean(tag);
            m_Renderer.sprite = m_Empty;
            return;
        }
        
        if(tag.Equals("undo_draw"))
        {
            m_IsEmpty = false;
            m_Renderer.sprite = m_Back;
            return;
        }
    }
}

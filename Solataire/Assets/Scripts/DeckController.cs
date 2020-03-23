using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField]private GameController m_GameController;
    [SerializeField]private SpriteRenderer m_Renderer;
    [SerializeField]private Sprite m_Back;
    [SerializeField]private Sprite m_Empty;
    [SerializeField]private sbyte m_CurrentDrawCard;
    [SerializeField]private bool isEmpty;

    // Start is called before the first frame update
    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_CurrentDrawCard = -1;
        isEmpty = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("DeckController onMouseDown");
        if (!isEmpty)
        {
            m_CurrentDrawCard++;
            m_GameController.DrawCardFromDeck(m_CurrentDrawCard);

            if(m_CurrentDrawCard >= m_GameController.DecksSize() - 1)
            {
                isEmpty = true;
                m_Renderer.sprite = m_Empty;
            }
        }
        else
        {
            m_CurrentDrawCard = -1;
            isEmpty = false;
            m_GameController.PutBackToDeck();
            m_Renderer.sprite = m_Back;
        }
    }
}

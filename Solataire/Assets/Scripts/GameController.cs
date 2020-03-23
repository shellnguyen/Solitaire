using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    [Flags]
    public enum CardValue
    {
        Ace = 0,
        Two = 1,
        Three = 1 << 1,
        Four = 1 << 2,
        Five = 1 << 3,
        Six = 1 << 4,
        Seven = 1 << 5,
        Eight = 1 << 6,
        Nine = 1 << 7,
        Ten = 1 << 8,
        Jack = 1 << 9,
        Queen = 1 << 10,
        King = 1 << 11
    }

    [Flags]
    public enum SuitType
    {
        Spades = 1 << 12,
        Clubs = 1 << 13,
        Diamonds = 1 << 14,
        Hearts = 1 << 15
    }

    [Flags]
    public enum CardPosition
    {
        Deck,
        Draw,
        Top1 = 8,
        Top2,
        Top3,
        Top4,
        Bottom1,
        Bottom2,
        Bottom3,
        Bottom4,
        Bottom5,
        Bottom6,
        Bottom7
    }
}

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData m_GameData;
    [SerializeField] private CommonData m_MainData;
    [SerializeField] private GameObject m_CardPrefab;
    [SerializeField] private List<CardElement> m_DeckCards;
    [SerializeField] private List<CardElement> m_BottomCards;
    [SerializeField] private List<CardElement> m_TopCards;
    [SerializeField] private GameObject[] m_BottomList;
    [SerializeField] private GameObject[] m_TopList;
    [SerializeField] private GameObject m_DeckButton;
    [SerializeField] private GameObject m_DrawCardHolder;
    public List<int> ListCards;

    private void Awake()
    {
        ListCards = new List<int>();
        m_DeckCards = m_GameData.deckCards = new List<CardElement>();
        m_BottomCards = m_GameData.bottomCards = new List<CardElement>();
        m_TopCards = m_GameData.topCards = new List<CardElement>();

        GenerateDeck();
        StartCoroutine(DealCards());
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void GenerateDeck()
    {
        StringBuilder builder = new StringBuilder();
        ushort suitOffset = 12;
        ushort cardOffset = 1;


        for (int i = 0; i < 4; ++i) //Suits
        {
            for (int k = 0; k < 13; ++k) //Card value
            {
                ushort suitValue = (ushort)(1 << (i + suitOffset));
                ushort cardValue = (ushort)((k == 0 ? 0 : 1) << (k - cardOffset));
                builder.Append(Enum.GetName(typeof(Solitaire.SuitType), suitValue));
                builder.Append("_");
                builder.Append(Enum.GetName(typeof(Solitaire.CardValue), cardValue));
                //Debug.Log("cardName = " + builder);

                GameObject card = Instantiate(m_CardPrefab, m_DeckButton.transform.position + Vector3.forward, Quaternion.identity);
                card.transform.SetParent(m_DeckButton.transform);

                m_DeckCards.Add(card.GetComponent<CardElement>());
                m_DeckCards[i * 13 + k].SetCardProperties(this, (ushort)(0 | suitValue | cardValue), builder.ToString());

                builder.Clear();
            }
        }

        Shuffle<CardElement>(m_DeckCards);
    }

    private IEnumerator DealCards()
    {
        int bottomNum = 1;
        int cardNum = 0;
        float yOffset = 0.0f;
        float zOffset = 0.0f;

        for (int i = 0; i < 28; ++i)
        {
            yield return new WaitForSeconds(0.05f);
            if (cardNum == bottomNum)
            {
                bottomNum++;
                cardNum = 0;
                yOffset = 0.0f;
                zOffset = 0.0f;
            }

            m_BottomCards.Add(m_DeckCards[i]);

            iTween.MoveTo(m_BottomCards[i].gameObject, new Vector3(m_BottomList[bottomNum - 1].transform.position.x, m_BottomList[bottomNum - 1].transform.position.y - yOffset, m_BottomList[bottomNum - 1].transform.position.z - zOffset), 0.05f);
            if (cardNum == bottomNum - 1)
            {
                m_BottomCards[i].isFaceUp = true;
            }
            m_BottomCards[i].position = (Solitaire.CardPosition)(bottomNum + 11); //TODO: remove hardcode index
            m_BottomCards[i].transform.SetParent(m_BottomList[bottomNum - 1].transform);
            cardNum++;
            yOffset += 0.3f;
            zOffset += 0.1f;
        }

        m_DeckCards.RemoveRange(0, 28);
    }

    public void DrawCardFromDeck()
    {
        StartCoroutine(DrawCard());
    }

    public void PutBackToDeck()
    {
        StartCoroutine(PutBackCard());
    }

    IEnumerator PutBackCard()
    {
        foreach (CardElement card in m_DeckCards)
        {
            iTween.MoveTo(card.gameObject, m_DeckButton.transform.position + Vector3.forward, 0.1f);
            card.position = Solitaire.CardPosition.Deck;
            card.isFaceUp = false;
        }

        yield break;
    }

    IEnumerator DrawCard()
    {
        float offSet = 0.5f;

        sbyte currentDrawCard = m_GameData.currentDrawCard;
        if (currentDrawCard > 2)
        {
            for (int i = currentDrawCard - 2; i < currentDrawCard; ++i)
            {
                iTween.MoveTo(m_DeckCards[i].gameObject, new Vector3(m_DeckCards[i].transform.position.x - offSet, m_DeckCards[i].transform.position.y, -(i * offSet)), 0.0f);
            }
        }
        iTween.MoveTo(m_DeckCards[currentDrawCard].gameObject, new Vector3(m_DrawCardHolder.transform.position.x + (currentDrawCard >= 2 ? 1.0f : currentDrawCard * offSet), m_DrawCardHolder.transform.position.y, -(currentDrawCard * offSet)), 0.1f);
        m_DeckCards[currentDrawCard].isFaceUp = true;
        m_DeckCards[currentDrawCard].position = Solitaire.CardPosition.Draw;

        yield break;
    }

    public bool CheckMoveCard(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Card":
                {

                    //element.m_CardValue = 1;
                    break;
                }
            case "Bottom":
                {
                    break;
                }
            case "Top":
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        return false;
    }

    public void Shuffle<T>(List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}

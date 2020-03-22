using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;

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
}

public class GameController : MonoBehaviour
{
    [SerializeField]private CommonData m_MainData;
    [SerializeField]private GameObject m_CardPrefab;
    [SerializeField]private List<CardElement> m_ListCards;
    [SerializeField]private GameObject[] m_BottomList;
    [SerializeField]private GameObject[] m_TopList;
    [SerializeField]private GameObject m_DeckButton;
    public List<int> ListCards;


    // Start is called before the first frame update
    private void Start()
    {
        ListCards = new List<int>();
        m_ListCards = new List<CardElement>();
        GenerateDeck();
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

        float xOffset = 0.0f;
        float yOffset = 0.0f;


        for(int i = 0; i < 4; ++i)
        {
            for(int k = 0; k < 13; ++k)
            {
                ushort suitValue = (ushort)(1 << (i + suitOffset));
                ushort cardValue = (ushort)((k == 0 ? 0 : 1) << (k - cardOffset));
                builder.Append(Enum.GetName(typeof(Solitaire.SuitType), suitValue));
                builder.Append("_");
                builder.Append(Enum.GetName(typeof(Solitaire.CardValue), cardValue));
                Debug.Log("cardName = " + builder);
                //ListCards.Add(0 | suitValue | cardValue);

                //CardElement card = new CardElement();
                GameObject tempCard = Instantiate(m_CardPrefab, m_DeckButton.transform.position, Quaternion.identity);


                m_ListCards.Add(tempCard.GetComponent<CardElement>());
                m_ListCards[i * 13 + k].SetCardProperties((ushort)(0 | suitValue | cardValue), builder.ToString());

                yOffset += 0.5f;
                //Debug.Log("Card = " + ListCards[i] + " - Binary = " + Convert.ToString(ListCards[i], 2));
                builder.Clear();
            }
        }

        Shuffle<CardElement>(m_ListCards);
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

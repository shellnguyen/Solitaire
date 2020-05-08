using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move
{
    //private List<CardElement> m_DeckCards;
    //private List<CardElement> m_BottomCards;
    //private List<CardElement> m_TopCards;
    private CardElement m_Card;
    private Transform m_PrevParent;
    private Vector3 m_PrevPosition;
    private Solitaire.CardPosition m_PrevCardPos;
    private CardElement m_PrevNextInStack;
    

    private sbyte m_CurrentDrawCard;
    private ushort m_Score;
    private ushort m_MoveNumber;

    //New move hold the current GameData state
    public Move(CardElement card, GameData data)
    {
        m_Card = card;
        m_PrevCardPos = card.position;
        m_PrevPosition = card.transform.position;
        m_PrevParent = card.transform.parent;
        m_PrevNextInStack = card.PrevInStack;

        m_CurrentDrawCard = data.currentDrawCard;
        m_Score = data.score;
        m_MoveNumber = data.move;
    }

    //Set GameData state to same as Move
    public void Execute(ref GameData data)
    {

        data.currentDrawCard = m_CurrentDrawCard;
        data.score = m_Score;
        data.move = m_MoveNumber;
    }
}

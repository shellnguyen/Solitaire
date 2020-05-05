using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    private List<CardElement> m_DeckCards;
    private List<CardElement> m_BottomCards;
    private List<CardElement> m_TopCards;

    private sbyte m_CurrentDrawCard;
    private ushort m_Score;
    private ushort m_MoveNumber;

    //New move hold the current GameData state
    public Move(GameData data)
    {
        m_DeckCards = data.deckCards;
        m_BottomCards = data.bottomCards;
        m_TopCards = data.topCards;

        m_CurrentDrawCard = data.currentDrawCard;
        m_Score = data.score;
        m_MoveNumber = data.move;
    }

    //Set GameData state to same as Move
    public void Execute(GameData data)
    {
        data.deckCards = m_DeckCards;
        data.bottomCards = m_BottomCards;
        data.topCards = m_TopCards;

        data.currentDrawCard = m_CurrentDrawCard;
        data.score = m_Score;
        data.move = m_MoveNumber;
    }
}

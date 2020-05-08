using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private CardElement m_Card;
    private Transform m_PrevParent;
    private Vector3 m_PrevPosition;
    private Solitaire.CardPosition m_PrevCardPos;
    private CardElement m_PrevInStack;
    private bool m_IsPrevFaceDown;


    private sbyte m_CurrentDrawCard;
    private ushort m_Score;
    private ushort m_MoveNumber;

    //New move hold the current GameData state
    public MoveCommand(CardElement card, GameData data)
    {
        m_Card = card;
        m_PrevCardPos = card.position;
        m_PrevPosition = card.transform.position;
        m_PrevParent = card.transform.parent;
        m_PrevInStack = card.PrevInStack;
        if(!m_PrevInStack.IsFaceUp)
        {
            m_IsPrevFaceDown = true;
        }
        else
        {
            m_IsPrevFaceDown = false;
        }

        m_CurrentDrawCard = data.currentDrawCard;
        m_Score = data.score;
        m_MoveNumber = data.move;
    }

    public void Execute()
    {
        //
    }

    public void Undo(GameData data)
    {
        // Previously from Bottom
        if ((m_PrevCardPos & (Solitaire.CardPosition.Bottom1 | Solitaire.CardPosition.Bottom2 | Solitaire.CardPosition.Bottom3 | Solitaire.CardPosition.Bottom4 | Solitaire.CardPosition.Bottom5 | Solitaire.CardPosition.Bottom6 | Solitaire.CardPosition.Bottom7)) != 0)
        {
            // Now from Top
            if ((m_Card.position & (Solitaire.CardPosition.Bottom1 | Solitaire.CardPosition.Bottom2 | Solitaire.CardPosition.Bottom3 | Solitaire.CardPosition.Bottom4 | Solitaire.CardPosition.Bottom5 | Solitaire.CardPosition.Bottom6 | Solitaire.CardPosition.Bottom7)) == 0)
            {
                data.topCards.Remove(m_Card);
                data.bottomCards.Add(m_Card);
                iTween.MoveTo(m_Card.gameObject, m_PrevPosition, Common.MOVE_TIME);
            }
            else
            {
                Utilities.Instance.MoveToWithCallBack(m_Card.gameObject, m_PrevPosition, Common.MOVE_TIME, "OnCardMove");
            }

            m_Card.PrevInStack = m_PrevInStack;
            if(m_IsPrevFaceDown)
            {
                m_Card.PrevInStack.gameObject.layer = 9;
                m_Card.PrevInStack.IsFaceUp = false;
            }
            m_Card.SetCardPosition(m_PrevCardPos);
            m_Card.SetCardParent(m_PrevParent);

            //m_Card
        }
        else //Previously from Draw
        {

        }

        data.score = m_Score;
        data.move = m_MoveNumber;

        //
        /*
        if((prevCardPos & (B1 | B2 | B3 | B4 | B5 | B6 | B7) != 0) )
            if((card.CardPos & (B1 | B2 | B3 | B4 | B5 | B6 | B7) == 0)) -> Move Bottom -> Top
                data.Top.remove(card)
                data.Bottom.add(card)

            card.Position = prevPosition
            card.CardPos = prevCardPos
            card.Parent = prevParent
            card.PrevInStack = prevInStack
        else
            if((card.CardPos & (B1 | B2 | B3 | B4 | B5 | B6 | B7) == 0)) -> Move Draw -> Top
                data.Top.remove(card)
            else -> Move Draw -> Bottom
                data.Bottom.remove(card)

            data.Deck.add(card)
            data.currentDrawCard = prevCDC
            Move other cards if more than 1 card is draw
            card.Position = prevPosition
            card.CardPos = prevCardPos
            card.Parent = prevParent
            card.PrevInStack = prevInStack
                
        */
    }
}

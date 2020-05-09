using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCommand : ICommand
{
    private Vector3 m_DeckPosition;
    private ushort m_PrevMoveNumber;

    public DrawCommand(Vector3 deckPos, GameData data)
    {
        m_DeckPosition = deckPos;
        m_DeckPosition.z = 1.0f;
        m_PrevMoveNumber = data.move;
    }

    public void Execute()
    {
        //
    }

    public void Undo(GameData data)
    {
        float offSet = 0.5f;
        //data.currentDrawCard = m_PrevCurrentDrawCard;
        if (data.currentDrawCard > 2)
        {
            for (int i = data.currentDrawCard - 2; i < data.currentDrawCard; ++i)
            {
                iTween.MoveTo(data.deckCards[i].gameObject, new Vector3(data.deckCards[i].transform.position.x + offSet, data.deckCards[i].transform.position.y, -(i * offSet)), 0.0f);
            }
        }

        iTween.MoveTo(data.deckCards[data.currentDrawCard].gameObject, m_DeckPosition, 0.1f);
        data.deckCards[data.currentDrawCard].IsFaceUp = false;
        data.deckCards[data.currentDrawCard].position = Solitaire.CardPosition.Deck;
        data.deckCards[data.currentDrawCard].gameObject.layer = 9;

        data.currentDrawCard--;
        data.move = m_PrevMoveNumber;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", data.move.ToString());

        /*
        if(currentDrawCard > 2)
            for(int i = currentDrawCard - 2; i < prevDrawCard; ++i)
                Move prevDrawCard back to their prev position
            
            Move currentDrawCard back to Deck
            Reset currentDrawCard cardPos, layer and IsFaceUp

            data.currentDrawCard--
        */
    }
}

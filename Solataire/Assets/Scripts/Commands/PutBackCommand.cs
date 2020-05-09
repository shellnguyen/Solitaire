using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBackCommand : ICommand
{
    private Vector3 m_DrawCardHolder;
    private ushort m_PrevMoveNumber;

    public PutBackCommand(Vector3 drawCardHolder, GameData data)
    {
        m_DrawCardHolder = drawCardHolder;
        m_PrevMoveNumber = data.move;
    }

    public void Execute()
    {
        //
    }

    public void Undo(GameData data)
    {
        float offSet = 0.5f;

        for(int i = 0; i < data.deckCards.Count; ++i)
        {
            if(i >= data.deckCards.Count - 2)
            {
                iTween.MoveTo(data.deckCards[i].gameObject, new Vector3(m_DrawCardHolder.x + (i == data.deckCards.Count - 1 ? 1.0f : 0.5f), m_DrawCardHolder.y, -(i * offSet)), 0.1f);
            }
            else
            {
                iTween.MoveTo(data.deckCards[i].gameObject, new Vector3( m_DrawCardHolder.x, m_DrawCardHolder.y, -(i * offSet)), 0.1f);
            }

            data.deckCards[i].IsFaceUp = true;
            data.deckCards[i].position = Solitaire.CardPosition.Draw;
            data.deckCards[i].gameObject.layer = 8;
        }

        data.currentDrawCard = (sbyte)(data.deckCards.Count - 1);
        data.move = m_PrevMoveNumber;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", data.move.ToString());
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "undo_putback", true);

        /*
        for(int i = data.deckCards.size - 1; i < 0; --i)
            if(i >= data.deckCards.size - 2)
                
        */
    }
}

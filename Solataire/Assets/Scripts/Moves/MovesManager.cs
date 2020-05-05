using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager
{
    private Stack<Move> m_Moves;

    public MovesManager()
    {
        m_Moves = new Stack<Move>();
    }

    public void AddMove(Move move)
    {
        m_Moves.Push(move);
    }

    public void Undo(GameData data)
    {
        m_Moves.Pop();
        m_Moves.Peek().Execute(data);
    }

    public bool CanUndo()
    {
        if(m_Moves.Count > 1)
        {
            return true;
        }
        return false;
    }

}

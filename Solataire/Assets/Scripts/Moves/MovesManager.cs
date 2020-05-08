using System.Collections.Generic;

[System.Serializable]
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

    public void Undo(ref GameData data)
    {
        if(CanUndo())
        {
            m_Moves.Pop();
            m_Moves.Peek().Execute(ref data);
        }
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

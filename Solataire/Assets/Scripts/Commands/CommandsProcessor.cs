using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsProcessor
{
    [SerializeField] private Stack<ICommand> m_Commands;

    public CommandsProcessor()
    {
        m_Commands = new Stack<ICommand>();
    }

    public void AddCommand(ICommand command)
    {
        if(m_Commands.Count == 0)
        {
            Utilities.Instance.DispatchEvent(Solitaire.Event.ChangeUIStatue, "undo", true);
        }
        m_Commands.Push(command);
    }

    public void ExecuteCommand()
    {
        m_Commands.Peek().Execute();
    }

    public void UndoCommand(GameData data)
    {
        m_Commands.Pop().Undo(data);

        if(m_Commands.Count <= 0)
        {
            Utilities.Instance.DispatchEvent(Solitaire.Event.ChangeUIStatue, "undo", false);
        }
    }

    public bool CanUndo()
    {
        if(m_Commands.Count > 0)
        {
            return true;
        }

        return false;
    }
}

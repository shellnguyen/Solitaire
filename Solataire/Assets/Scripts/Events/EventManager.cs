using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    //Singleton Init
    private static readonly EventManager instance = new EventManager();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EventManager()
    {
    }

    private EventManager()
    {
    }

    public static EventManager Instance
    {
        get
        {
            return instance;
        }
    }
    //

    [SerializeField] private List<EventTemplate> m_EventList;

    public void RaiseEvent(Solitaire.Event eventId, EventParam param)
    {
        int index = (int)eventId;
        m_EventList[index].Raise(param);
    }
}

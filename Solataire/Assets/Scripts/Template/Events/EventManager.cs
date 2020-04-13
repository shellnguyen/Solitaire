using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="EventManagerTemplate", menuName ="Template/Events/EventManager")]
public class EventManager : ScriptableObject
{
    [SerializeField] private List<EventTemplate> m_EventList;

    public void Register(Solitaire.Event eventId, UnityAction<EventParam> callback)
    {
        int index = (int)eventId;
        m_EventList[index].Register(callback);
    }
    
    public void Unregister(Solitaire.Event eventId, UnityAction<EventParam> callback)
    {
        int index = (int)eventId;
        m_EventList[index].Unresigter(callback);
    }

    public void RaiseEvent(Solitaire.Event eventId, EventParam param)
    {
        int index = (int)eventId;
        m_EventList[index].Raise(param);
    }
}

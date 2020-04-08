using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EventManagerTemplate", menuName ="Template/Events/EventManager")]
public class EventManager : ScriptableObject
{
    [SerializeField] private List<EventTemplate> m_EventList;

    public void RaiseEvent(Solitaire.Event eventId, EventParam param)
    {
        int index = (int)eventId;
        m_EventList[index].Raise(param);
    }
}

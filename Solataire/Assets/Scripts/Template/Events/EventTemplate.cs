using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="EventTemplate", menuName ="Template/Events/EventTemplate")]
public class EventTemplate : ScriptableObject
{
    public int id;
    private EventHandler m_Handler = new EventHandler();

    public void Register(UnityAction<EventParam> listener)
    {
        m_Handler.AddListener(listener);
    }

    public void Unresigter(UnityAction<EventParam> listener)
    {
        m_Handler.RemoveListener(listener);
    }

    public void Raise(EventParam param)
    {
        m_Handler.Invoke(param);
    }
}

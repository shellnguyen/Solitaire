using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EventTemplate", menuName ="Template/Event/EventTemplate")]
public class EventTemplate : ScriptableObject
{
    public int id;
    private List<EventListener> m_Listeners = new List<EventListener>();

    public void Register(EventListener listener)
    {
        m_Listeners.Add(listener);
    }

    public void Unregister(EventListener listener)
    {
        m_Listeners.Remove(listener);
    }

    public void Raise(EventParam param)
    {
        foreach(EventListener listener in m_Listeners)
        {
            listener.OnRaised(param);
        }
    }
}

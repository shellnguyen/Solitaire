using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    protected EventTemplate m_Event;
    protected UnityEvent<EventParam> m_Handler;

    // Start is called before the first frame update
    private void OnEnable()
    {
        m_Event.Register(this);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        m_Event.Unregister(this);
    }

    public void OnRaised(EventParam param)
    {
        m_Handler.Invoke(param);
    }
}

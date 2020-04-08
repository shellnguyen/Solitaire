using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField]protected EventTemplate m_Event;
    protected EventHandler m_Handler;

    // Start is called before the first frame update
    protected void OnEnable()
    {
        m_Event.Register(this);
        m_Handler = new EventHandler();
    }

    // Update is called once per frame
    protected void OnDisable()
    {
        m_Event.Unregister(this);
    }

    public void OnRaised(EventParam param)
    {
        m_Handler.Invoke(param);
    }
}

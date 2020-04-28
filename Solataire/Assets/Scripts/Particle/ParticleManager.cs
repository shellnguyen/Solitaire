using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject m_FireworkLeft;
    [SerializeField] private GameObject m_FireworkRight;

    private void OnEnable()
    {
        EventManager.Instance.Register(Solitaire.Event.PlayEffect, OnPlayEffect);
        EventManager.Instance.Register(Solitaire.Event.OnNewGame, OnNewGame);
        //Utilities.Instance.DispatchEvent(Solitaire.Event.PlayEffect, "firework", "");
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.PlayEffect, OnPlayEffect);
        EventManager.Instance.Unregister(Solitaire.Event.OnNewGame, OnNewGame);
    }

    private void OnNewGame(EventParam param)
    {
        m_FireworkLeft.SetActive(false);
        m_FireworkRight.SetActive(false);
    }

    private void OnPlayEffect(EventParam param)
    {
        string tag = param.GetString("tag");

        switch(tag)
        {
            case "firework":
                {
                    m_FireworkLeft.SetActive(true);
                    m_FireworkRight.SetActive(true);
                    break;
                }
        }
    }
}

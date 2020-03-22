using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardElement : MonoBehaviour
{
    [SerializeField]private Sprite m_Front;
    [SerializeField]private Sprite m_Back;
    [SerializeField]private SpriteAtlas m_SpriteAtlas;
    private SpriteRenderer m_Renderer;
    [SerializeField]private ushort m_CardValue;
    [SerializeField]private string m_CardName;
    public bool isFaceUp = false;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if(isFaceUp)
        {
            m_Renderer.sprite = m_Front;
        }
        else
        {
            m_Renderer.sprite = m_Back;
        }
    }

    public void SetCardProperties(ushort cardValue, string cardName)
    {
        m_CardName = cardName;
        m_CardValue = cardValue;

        m_Front = m_SpriteAtlas.GetSprite(m_CardName);
    }
}

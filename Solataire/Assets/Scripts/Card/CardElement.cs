using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardElement : MonoBehaviour
{
    [SerializeField]private GameController m_GameController;
    [SerializeField]private Sprite m_Front;
    [SerializeField]private Sprite m_Back;
    [SerializeField]private SpriteAtlas m_SpriteAtlas;
    [SerializeField]private SpriteRenderer m_Renderer;
    [SerializeField]private ushort m_CardValue;
    [SerializeField]private string m_CardName;
    private Vector3 m_PrevPos;
    private bool m_IsNewPosValid;
    public bool isFaceUp = false;
    public Solitaire.CardPosition position;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_PrevPos = Vector3.zero;
        m_IsNewPosValid = false;
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

    public void SetCardProperties(GameController controller, ushort cardValue, string cardName)
    {
        m_GameController = controller;
        m_CardName = cardName;
        m_CardValue = cardValue;

        m_Front = m_SpriteAtlas.GetSprite(m_CardName);
        position = Solitaire.CardPosition.Deck;
    }

    private void OnMouseDown()
    {
        Debug.Log("Down Name = " + m_CardName);
        m_PrevPos = this.transform.position;
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag Name = " + m_CardName);
        Vector3 mousePos = Utilities.Instance.GetWorldPosition2D(Input.mousePosition);
        mousePos.z = -15.0f;

        this.gameObject.transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        Debug.Log("Up Name = " + m_CardName);
        if(!m_IsNewPosValid)
        {
            this.transform.position = m_PrevPos;
        }
        else
        {
            m_IsNewPosValid = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}

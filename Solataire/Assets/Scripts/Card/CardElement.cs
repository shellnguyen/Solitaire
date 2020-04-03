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
    [SerializeField]private CardElement m_PrevFaceDown;
    [SerializeField]private CardElement m_NextInStack;
    [SerializeField]private GameObject m_Collided;
    private Vector3 m_PrevPos;
    private bool m_IsNewPosValid;
    [SerializeField]private bool m_IsSelected;
    private bool m_IsFaceUp;
    [SerializeField]private byte m_CollidedTag;
    [SerializeField]private bool m_IsDragging;
    public Solitaire.CardPosition position;

    public bool IsSelected
    {
        get
        {
            return m_IsSelected;
        }
        set
        {
            m_IsSelected = value;
            OnSelectedChange();
        }
    }

    public bool IsFaceUp
    {
        get
        {
            return m_IsFaceUp;
        }
        set
        {
            m_IsFaceUp = value;
            OnCardFaceChanged();
        }
    }

    public ushort CardValue
    {
        get
        {
            return m_CardValue;
        }
    }

    public bool IsNewPosValid
    {
        get
        {
            return m_IsNewPosValid;
        }

        set
        {
            m_IsNewPosValid = value;
        }
    }

    public byte CollidedTag
    {
        get
        {
            return m_CollidedTag;
        }

        set
        {
            m_CollidedTag = value;
        }
    }

    public bool IsDragging
    {
        get
        {
            return m_IsDragging;
        }

        set
        {
            m_IsDragging = value;
        }
    }

    public GameObject Collided
    {
        get
        {
            return m_Collided;
        }

        set
        {
            m_Collided = value;
        }
    }

    public string CardName
    {
        get
        {
            return m_CardName;
        }

        set
        {
            m_CardName = value;
        }
    }

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_NextInStack = null;
        m_PrevFaceDown = null;
        m_PrevPos = Vector3.zero;
        m_IsNewPosValid = false;
        m_IsSelected = false;
        m_IsDragging = false;
        m_CollidedTag = 0;
        m_Collided = null;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetCardProperties(GameController controller, ushort cardValue, string cardName)
    {
        m_GameController = controller;
        m_CardName = cardName;
        m_CardValue = cardValue;

        m_Front = m_SpriteAtlas.GetSprite(m_CardName);
        position = Solitaire.CardPosition.Deck;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_IsSelected)
        {
            return;
        }

        switch(collision.tag)
        {
            case "Card":
                {

                    CardElement card = collision.gameObject.GetComponent<CardElement>();
                    Debug.Log("card " + m_CardName + " OnTriggerEnter2D with " + card.CardName);
                    if (!card.IsInStack() && card.IsFaceUp)
                    {
                        m_CollidedTag = 1;
                        m_Collided = collision.gameObject;
                    }
                    break;
                }
            case "Top":
                {
                    Debug.Log("card " + m_CardName + " OnTriggerEnter2D with " + collision.gameObject.name);
                    if (collision.gameObject.transform.childCount == 0)
                    {
                        m_CollidedTag = 2;
                        m_Collided = collision.gameObject;
                    }
                    break;
                }
            case "Bottom":
                {
                    Debug.Log("card " + m_CardName + " OnTriggerEnter2D with " + collision.gameObject.name);
                    if (collision.gameObject.transform.childCount == 0)
                    {
                        m_CollidedTag = 3;
                        m_Collided = collision.gameObject;
                    }
                    else
                    {
                        //TODO: any better way to do this
                        //GameObject lastChild = collision.transform.GetChild(collision.transform.childCount - 1).gameObject;
                        //CardElement card = lastChild.GetComponent<CardElement>();

                        //if (!card.IsInStack() && card.IsFaceUp && card.position != this.position)
                        //{
                        //    m_CollidedTag = 1;
                        //    m_Collided = lastChild;
                        //}
                    }
                    break;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!m_IsSelected)
        {
            return;
        }
        Debug.Log("card " + m_CardName + " OnTriggerExit2D");

        if(m_Collided && m_Collided.GetInstanceID() == collision.gameObject.GetInstanceID())
        {
            Debug.Log("move out of m_Collided");
            m_CollidedTag = 0;
            m_Collided = null;
        }
    }

    public bool IsInStack()
    {
        return m_NextInStack ? true : false;
    }

    public bool HasPrevFaceDown()
    {
        return m_PrevFaceDown ? true : false;
    }

    public void OnSelectedChange()
    {
        if (m_IsSelected)
        {
            m_Renderer.color = Color.yellow;
            m_PrevPos = this.transform.position;
        }
        else
        {
            m_Renderer.color = Color.white;
            m_PrevPos = Vector3.zero;
        }

        if(m_NextInStack && ((m_NextInStack.position & this.position) > 0))
        {
            if(((m_NextInStack.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) == 0))
            {
                m_NextInStack.IsSelected = m_IsSelected;
                
            }
        }
        else
        {
            m_NextInStack = null;
        }
    }

    public void OnCardFaceChanged()
    {
        if (IsFaceUp)
        {
            m_Renderer.sprite = m_Front;
        }
        else
        {
            m_Renderer.sprite = m_Back;
        }
    }

    public void RemoveCardFromList(ref List<CardElement> listCard)
    {
        listCard.Remove(this);
        if(m_NextInStack)
        {
            m_NextInStack.RemoveCardFromList(ref listCard);
        }
    }

    public void AddCardToList(ref List<CardElement> listCard)
    {
        listCard.Add(this);
        if(m_NextInStack)
        {
            m_NextInStack.AddCardToList(ref listCard);
        }
    }

    public void OnCardMove()
    {
        if (m_NextInStack && ((m_NextInStack.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) == 0))
        {
            m_NextInStack.transform.position = new Vector3(transform.position.x, transform.position.y - Common.YOFFSET, transform.position.z - Common.ZOFFSET);
            m_NextInStack.OnCardMove();
        }
    }

    public void SetNextInStack(CardElement card)
    {
        m_NextInStack = card;
    }

    public void SetPrevFaceDown(CardElement card)
    {
        m_PrevFaceDown = card;
    }

    public void SetCardPosition(Solitaire.CardPosition position)
    {
        this.position = position;
        if(m_NextInStack)
        {
            m_NextInStack.SetCardPosition(position);
        }
    }

    public void SetCardParent(Transform parent)
    {
        if(this.transform.parent.childCount == 1)
        {
            this.transform.parent.gameObject.layer = 8;
        }

        this.transform.SetParent(parent);
        if(m_NextInStack)
        {
            m_NextInStack.SetCardParent(parent);
        }
    }

    public void FlipPreDownCard()
    {
        if(m_PrevFaceDown)
        {
            m_PrevFaceDown.gameObject.layer = 8;
            m_PrevFaceDown.IsFaceUp = true;
            m_PrevFaceDown = null;
        }
    }

    public void OnCardDrag(Vector3 position)
    {
        transform.position = position;
        if(m_NextInStack)
        {
            position.y -= Common.YOFFSET;
            position.z -= Common.ZOFFSET;
            m_NextInStack.OnCardDrag(position);
        }
    }

    public void ResetCardPosition()
    {
        this.transform.position = m_PrevPos;
        m_PrevPos = Vector3.zero;
        IsSelected = false;
        m_IsDragging = false;
        m_CollidedTag = 0;
        m_Collided = null;

        if(m_NextInStack)
        {
            m_NextInStack.ResetCardPosition();
        }
    }

    public void EndDragging()
    {
        m_PrevPos = Vector3.zero;
        m_IsDragging = false;
        m_CollidedTag = 0;
        m_Collided = null;

        if (m_NextInStack)
        {
            m_NextInStack.EndDragging();
        }
    }

    public CardElement GetLastCardInStack()
    {
        if(!m_NextInStack)
        {
            return this;
        }
        else
        {
            return m_NextInStack.GetLastCardInStack();
        }
    }
}

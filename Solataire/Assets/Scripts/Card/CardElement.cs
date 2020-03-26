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
    private Vector3 m_PrevPos;
    private bool m_IsNewPosValid;
    private bool m_IsSelected;
    private bool m_IsFaceUp;
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

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_NextInStack = null;
        m_PrevFaceDown = null;
        m_PrevPos = Vector3.zero;
        m_IsNewPosValid = false;
        m_IsSelected = false;
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

    //private void OnMouseDown()
    //{
    //    if(!IsFaceUp && (position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) >= 0)
    //    {
    //        return;
    //    }
    //    Debug.Log("Down Name = " + m_CardName);
    //    m_PrevPos = this.transform.position;
    //    //IsSelected = true;
    //    m_GameController.SetCurrentCard(this);
    //}

    //private void OnMouseDrag()
    //{
    //    if (!IsSelected)
    //    {
    //        return;
    //    }

    //    Debug.Log("Drag Name = " + m_CardName);
    //    Vector3 mousePos; //= Utilities.Instance.GetWorldPosition2D(Input.mousePosition);
    //    mousePos.z = -1.5f;

    //    //this.gameObject.transform.position = mousePos;    
    //}

    //private void OnMouseUp()
    //{
    //    if (!m_IsSelected)
    //    {
    //        return;
    //    }

    //    Debug.Log("Up Name = " + m_CardName);
    //    if (!m_IsNewPosValid)
    //    {
    //        this.transform.position = m_PrevPos;
    //    }
    //    else
    //    {
    //        m_IsNewPosValid = false;
    //    }

    //    //m_IsSelected = false;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!IsFaceUp && (position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) >= 0)
    //    {
    //        return;
    //    }
    //    m_GameController.CheckMoveCard(this, collision);
    //}

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
        if(!m_IsSelected)
        {
            m_Renderer.color = Color.white;
            if(m_NextInStack)
            {
                m_NextInStack.IsSelected = false;
            }
            //this.transform.position = m_PrevPos;
        }
        else
        {
            m_Renderer.color = Color.yellow;
            if (m_NextInStack)
            {
                m_NextInStack.IsSelected = true;
            }
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

    public void OnCardMove()
    {
        if(m_NextInStack)
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

    public void FlipPreDownCard()
    {
        if(m_PrevFaceDown)
        {
            m_PrevFaceDown.IsFaceUp = true;
            m_PrevFaceDown = null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    [Flags]
    public enum CardValue
    {
        Ace = 0,
        Two = 1,
        Three = 1 << 1,
        Four = 1 << 2,
        Five = 1 << 3,
        Six = 1 << 4,
        Seven = 1 << 5,
        Eight = 1 << 6,
        Nine = 1 << 7,
        Ten = 1 << 8,
        Jack = 1 << 9,
        Queen = 1 << 10,
        King = 1 << 11
    }

    [Flags]
    public enum SuitType
    {
        Spades = 1 << 12,
        Clubs = 1 << 13,
        Diamonds = 1 << 14,
        Hearts = 1 << 15
    }

    [Flags]
    public enum CardPosition
    {
        Deck = 1 << 8,
        Draw = 1 << 9,
        Top1 = 1 << 10,
        Top2 = 1 << 11,
        Top3 = 1 << 12,
        Top4 = 1 << 13,
        Bottom1 = 1 << 14,
        Bottom2 = 1 << 15,
        Bottom3 = 1 << 16,
        Bottom4 = 1 << 17,
        Bottom5 = 1 << 18,
        Bottom6 = 1 << 19,
        Bottom7 = 1 << 20
    }
}

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private GameData m_GameData;
    [SerializeField] private CommonData m_MainData;
    [SerializeField] private GameObject m_CardPrefab;
    [SerializeField] private List<CardElement> m_DeckCards;
    [SerializeField] private List<CardElement> m_BottomCards;
    [SerializeField] private List<CardElement> m_TopCards;
    [SerializeField] private GameObject[] m_BottomList;
    [SerializeField] private GameObject[] m_TopList;
    [SerializeField] private GameObject m_DeckButton;
    [SerializeField] private GameObject m_DrawCardHolder;
    [SerializeField] private CardElement m_CurrentSelected;
    [SerializeField] private bool m_IsWin;
    public List<int> ListCards;

    private void Awake()
    {
        ListCards = new List<int>();
        m_DeckCards = m_GameData.deckCards = new List<CardElement>();
        m_BottomCards = m_GameData.bottomCards = new List<CardElement>();
        m_TopCards = m_GameData.topCards = new List<CardElement>();
        m_CurrentSelected = null;
        m_IsWin = false;
        GenerateDeck();
        StartCoroutine(DealCards());
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Left mouse clicked
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("GameController MouseButton down");
            RaycastHit2D hit = Physics2D.Raycast(m_MainCamera.ScreenToWorldPoint(mousePos), Vector2.zero);

            if(hit)
            {
                switch(hit.collider.tag)
                {
                    case "Card":
                        {
                            CardElement card = hit.collider.gameObject.GetComponent<CardElement>();

                            //No selected and card not in top position
                            if(!m_CurrentSelected && (card.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) == 0)
                            {
                                if(((card.position & Solitaire.CardPosition.Draw) > 0) && card.CardValue != m_DeckCards[m_GameData.currentDrawCard].CardValue)
                                {
                                    return;
                                }

                                m_CurrentSelected = card;
                                m_CurrentSelected.IsSelected = true;
                                break;
                            }

                            //Card in Top position
                            if((card.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) > 0)
                            {
                                if(CanStack(m_CurrentSelected.CardValue, card.CardValue, true) && !m_CurrentSelected.IsInStack())
                                {
                                    StackToCard(card, true);
                                    //TODO: check win condition
                                    return;
                                }
                            }

                            //Card in Bottom position
                            if ((card.position & (Solitaire.CardPosition.Bottom1 | Solitaire.CardPosition.Bottom2 | Solitaire.CardPosition.Bottom3 | Solitaire.CardPosition.Bottom4 | Solitaire.CardPosition.Bottom5 | Solitaire.CardPosition.Bottom6 | Solitaire.CardPosition.Bottom7)) > 0)
                            {
                                if(CanStack(m_CurrentSelected.CardValue, card.CardValue))
                                {
                                    StackToCard(card, false);
                                    return;
                                }
                                else
                                {
                                    m_CurrentSelected.IsSelected = false;
                                    m_CurrentSelected = card;
                                    m_CurrentSelected.IsSelected = true;
                                    return;
                                }
                            }

                            //Card in draw position
                            if ((card.position & Solitaire.CardPosition.Draw) > 0)
                            {
                                if(card.CardValue == m_DeckCards[m_GameData.currentDrawCard].CardValue)
                                {
                                    m_CurrentSelected.IsSelected = false;
                                    m_CurrentSelected = card;
                                    m_CurrentSelected.IsSelected = true;
                                }
                            }

                                /*
                                if(!selected && (card.position & (CardPosition.Top1 | CardPosition.Top2 | CardPostion.Top3 | CardPositon.Top4)) == 0)
                                    if((card.positon & CardPosition.Draw) > 0 && card.CardValue != m_DeckCards[m_GameData.currentDrawCard].CardValue)
                                        return

                                    selected = card
                                    selected.IsSelect = true
                                    return

                                if(card.position & (CardPosition.Top1 | CardPosition.Top2 | CardPostion.Top3 | CardPositon.Top4) > 0)
                                    if(!selected)
                                        return

                                    if(CanStack(selected.CardValue, card.CardValue) && !selected.IsInStack())
                                        move selected to card position
                                        remove seleteced from current ListCard
                                        add selected to TopCards
                                        selected.IsSelect = false
                                        set selected = null
                                        CheckWinCondition()
                                        return

                                if((card.position & (CardPosition.Bottom1 | CardPosition.Bottom2 | CardPosition.Bottom3 | CardPosition.Bottom4 | CardPosition.Bottom5 | CardPosition.Bottom6 | CardPosition.Bottom7)) > 0)
                                    if(!selected)
                                        selected = card
                                        selected.IsSelect = true
                                        return

                                    if(CanStack(selected.CardValue, card.CardValue))
                                        move selected to card position
                                        remove seleteced from current ListCard
                                        add selected to TopCards
                                        selected.IsSelect = false
                                        set selected = null
                                        return


                                if((card.position & CardPosition.Draw) > 0)
                                    if(!selected && (card.CardValue == m_MainData.currentDrawCard.CardValue))
                                        selected = card
                                        selected.IsSelect = true
                                        return
                                */
                                break;
                        }
                    case "Bottom":
                        {
                            if (!m_CurrentSelected)
                            {
                                return;
                            }

                            ushort selectedCardValue = (ushort)Utilities.Instance.ExtractBit(m_CurrentSelected.CardValue, 12, 1);

                            if (selectedCardValue == (ushort)Solitaire.CardValue.King)
                            {
                                StackToPosition(hit.collider.gameObject, false);
                                return;
                            }
                            /*
                            if(!selected)
                                return
                            
                            selectedValue = Utilities.Instance.ExtractBit(selected, 12, 1);
                            if(selectedValue == CardValue.King)
                                move selected to bottom position
                                remove selected from current ListCard
                                add selected to BottomCards
                                set selected = null
                            */
                            break;
                        }
                    case "Top":
                        {
                            if (!m_CurrentSelected)
                            {
                                return;
                            }

                            ushort selectedCardValue = (ushort)Utilities.Instance.ExtractBit(m_CurrentSelected.CardValue, 12, 1);

                            if (selectedCardValue == (ushort)Solitaire.CardValue.Ace)
                            {
                                StackToPosition(hit.collider.gameObject, true);
                                return;
                            }
                            /*
                            if(!selected)
                                return
                            
                            selectedValue = Utilities.Instance.ExtractBit(selected, 12, 1);
                            if(selectedValue == CardValue.King)
                                move selected to top position
                                remove selected from current ListCard
                                add selected to TopCards
                                set selected = null
                            */
                            break;
                        }
                    default:
                        {
                            if(m_CurrentSelected)
                            {
                                m_CurrentSelected.IsSelected = false;
                                m_CurrentSelected = null;
                            }
                            break;
                        }
                }
            }
        }

        //Left mouse held down
        if(Input.GetMouseButton(0))
        {
            Debug.Log("GameController MouseButton drag");

            if(m_CurrentSelected)
            {
                Vector3 cardNewPos = Utilities.Instance.GetWorldPosition2D(m_MainCamera, mousePos);
                cardNewPos.z = -Common.DRAGGING_Z;
                m_CurrentSelected.IsDragging = true;
                m_CurrentSelected.OnCardDrag(cardNewPos);
            }
            /*

            if(selected)
                Vector3 cardNewPos = Utilities.Instance.GetWorldPosition2D(mousePos)
                selected.IsDragging = true;
                selected.OnCardDrag(cardNewPos)                    
            */
        }

        //Left mouse release
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("GameController MouseButton up");
            if(m_CurrentSelected && m_CurrentSelected.IsDragging)
            {
                switch(m_CurrentSelected.CollidedTag)
                {
                    case 1: //Card
                        {
                            if(!m_CurrentSelected.Collided)
                            {
                                return;
                            }
                            CardElement card = m_CurrentSelected.Collided.GetComponent<CardElement>();
                            //Card in Top position
                            if ((card.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) > 0)
                            {
                                if (CanStack(m_CurrentSelected.CardValue, card.CardValue, true) && !m_CurrentSelected.IsInStack())
                                {
                                    StackToCard(card, true);
                                    //TODO: check win condition
                                    return;
                                }
                            }

                            //Card in Bottom position
                            if ((card.position & (Solitaire.CardPosition.Bottom1 | Solitaire.CardPosition.Bottom2 | Solitaire.CardPosition.Bottom3 | Solitaire.CardPosition.Bottom4 | Solitaire.CardPosition.Bottom5 | Solitaire.CardPosition.Bottom6 | Solitaire.CardPosition.Bottom7)) > 0)
                            {
                                if (CanStack(m_CurrentSelected.CardValue, card.CardValue))
                                {
                                    StackToCard(card, false);
                                    return;
                                }
                                else
                                {
                                    m_CurrentSelected.IsSelected = false;
                                    m_CurrentSelected = card;
                                    m_CurrentSelected.IsSelected = true;
                                    return;
                                }
                            }
                            break;
                        }
                    case 2: //Top
                        {
                            if (!m_CurrentSelected)
                            {
                                return;
                            }

                            ushort selectedCardValue = (ushort)Utilities.Instance.ExtractBit(m_CurrentSelected.CardValue, 12, 1);

                            if (selectedCardValue == (ushort)Solitaire.CardValue.Ace)
                            {
                                StackToPosition(m_CurrentSelected.Collided, true);
                                return;
                            }
                            break;
                        }
                    case 3: //Bottom
                        {
                            if (!m_CurrentSelected)
                            {
                                return;
                            }

                            ushort selectedCardValue = (ushort)Utilities.Instance.ExtractBit(m_CurrentSelected.CardValue, 12, 1);

                            if (selectedCardValue == (ushort)Solitaire.CardValue.King)
                            {
                                StackToPosition(m_CurrentSelected.Collided, false);
                                return;
                            }
                            break;
                        }
                }

                m_CurrentSelected.ResetCardPosition();
                m_CurrentSelected = null;
            }

            /*
            if(selected && IsDragging)
                switch(selected.CollidedTag)
                    case 1: //Card
                        CardElement card = selected.Collided.GetComponent<CardElement>()
                        if(card.position & (CardPosition.Top1 | CardPosition.Top2 | CardPostion.Top3 | CardPositon.Top4) > 0)
                            if(!selected)
                                return

                            if(CanStack(selected.CardValue, card.CardValue) && !selected.IsInStack())
                                move selected to card position
                                remove seleteced from current ListCard
                                add selected to TopCards
                                selected.IsSelect = false
                                set selected = null
                                CheckWinCondition()
                                return

                        if((card.position & (CardPosition.Bottom1 | CardPosition.Bottom2 | CardPosition.Bottom3 | CardPosition.Bottom4 | CardPosition.Bottom5 | CardPosition.Bottom6 | CardPosition.Bottom7)) > 0)
                            if(!selected)
                                selected = card
                                selected.IsSelect = true
                                return

                            if(CanStack(selected.CardValue, card.CardValue))
                                move selected to card position
                                remove seleteced from current ListCard
                                add selected to TopCards
                                selected.IsSelect = false
                                set selected = null
                                return
                        break;
                        
                    case 2: //Top
                        selectedValue = Utilities.Instance.ExtractBit(selected, 12, 1);
                            if(selectedValue == CardValue.King)
                                move selected to top position
                                remove selected from current ListCard
                                add selected to TopCards
                                set selected = null
                                return
                        break

                    case 3: //Bottom
                        selectedValue = Utilities.Instance.ExtractBit(selected, 12, 1);
                            if(selectedValue == CardValue.King)
                                move selected to bottom position
                                remove selected from current ListCard
                                add selected to BottomCards
                                set selected = null
                                return
                        break

                selected.ResetCardPosition()
                selected = null
            */
        }
    }

    private bool CanStack(ushort selected, ushort target, bool isStackTop = false)
    {
        ushort selectedValue = (ushort)Utilities.Instance.ExtractBit(selected, 12, 1);
        ushort selectedSuit = (ushort)Utilities.Instance.ExtractBit(selected, 4, 13);
        ushort targetValue = (ushort)Utilities.Instance.ExtractBit(target, 12, 1);
        ushort targetSuit = (ushort)Utilities.Instance.ExtractBit(target, 4, 13);

        if(!isStackTop)
        {
            //selectedSuit OR targetSuit and check any bit is adjacent
            ushort suit = (ushort)Utilities.Instance.CheckAdjacentBit(selectedSuit | targetSuit);
            //CardValue: right shift 1 bit of target card value and if == selectedCard.value => OK 
            //CardSuit: suit == 0 => 2 card is not both black/red. special case suit == 2 (meaning Clubs + Diamonds). 
            if ((selectedValue == (targetValue >> 1)) && (targetSuit != selectedSuit) && (suit == 2 || suit == 0))
            {
                return true;
            }
        }
        else
        {
            //Check for when stack card to TopCard \>_</
            //CardValue: left shift 1 bit of target card value and if == selectedCard.value => OK. Special case when target card = Ace => selectedValue - targetValue = 1
            //CardSuit: suit must be the same 
            if (((selectedValue - targetValue == 1) || (selectedValue == (targetValue << 1))) && (selectedSuit == targetSuit))
            {
                return true;
            }
        }

        return false;
    }

    private void StackToCard(CardElement target, bool isStackToTop)
    {
        m_CurrentSelected.IsSelected = false;
        m_CurrentSelected.FlipPreDownCard();
        target.SetNextInStack(m_CurrentSelected); //Add current selected to stack of target card
        if ((m_CurrentSelected.position & Solitaire.CardPosition.Draw) > 0)
        {
            if(m_GameData.currentDrawCard > 2 && m_DeckCards.Count > 2)
            {
                for(int i = m_GameData.currentDrawCard - 1; i > m_GameData.currentDrawCard - 3; --i)
                {
                    iTween.MoveTo(m_DeckCards[i].gameObject, new Vector3(m_DeckCards[i].transform.position.x + Common.XOFFSET, m_DeckCards[i].transform.position.y, m_DeckCards[i].transform.position.z), Common.MOVE_TIME);
                }
            }
            m_DeckCards.Remove(m_CurrentSelected);
            m_GameData.currentDrawCard--;
        }
        else
        {   
            //TODO: any better way to check what list the card need to remove/add from/to
            if(isStackToTop)
            {
                m_BottomCards.Remove(m_CurrentSelected);
            }
        }

        if(isStackToTop)
        {
            m_TopCards.Add(m_CurrentSelected);
            iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
            CheckWinCondition();
        }
        else
        {
            if(m_CurrentSelected.position < Solitaire.CardPosition.Bottom1)
            {
                m_BottomCards.Add(m_CurrentSelected);
            }
            //iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3(target.transform.position.x, target.transform.position.y - Common.YOFFSET, target.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
            Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y - Common.YOFFSET, target.transform.position.z - Common.ZOFFSET);
            Utilities.Instance.MoveToWithCallBack(m_CurrentSelected.gameObject, newPos, Common.MOVE_TIME, "OnCardMove");
        }

        m_CurrentSelected.position = target.position;
        m_CurrentSelected.transform.SetParent(target.transform.parent);
        m_CurrentSelected = null;
    }

    private void StackToPosition(GameObject positionObj, bool isStackToTop)
    {
        ushort cardPos;
        ushort.TryParse(positionObj.name.Substring(positionObj.name.Length - 1, 1), out cardPos);
        m_CurrentSelected.IsSelected = false;
        m_CurrentSelected.FlipPreDownCard();
        if ((m_CurrentSelected.position & Solitaire.CardPosition.Draw) > 0)
        {
            if (m_GameData.currentDrawCard > 2 && m_DeckCards.Count > 2)
            {
                for (int i = m_GameData.currentDrawCard - 1; i > m_GameData.currentDrawCard - 3; --i)
                {
                    iTween.MoveTo(m_DeckCards[i].gameObject, new Vector3(m_DeckCards[i].transform.position.x + Common.XOFFSET, m_DeckCards[i].transform.position.y, m_DeckCards[i].transform.position.z), Common.MOVE_TIME);
                }
            }
            m_DeckCards.Remove(m_CurrentSelected);
            m_GameData.currentDrawCard--;
        }
        else
        {
            if (isStackToTop)
            {
                m_BottomCards.Remove(m_CurrentSelected);
            }
        }

        if(isStackToTop)
        {
            m_TopCards.Add(m_CurrentSelected);
            m_CurrentSelected.position = (Solitaire.CardPosition)(1 << (cardPos + 9));
        }
        else
        {
            if (m_CurrentSelected.position < Solitaire.CardPosition.Bottom1)
            {
                m_BottomCards.Add(m_CurrentSelected);
            }
            m_CurrentSelected.position = (Solitaire.CardPosition)(1 << (cardPos + 13));
        }

        //iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3(positionObj.transform.position.x, positionObj.transform.position.y, positionObj.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
        Vector3 newPos = new Vector3(positionObj.transform.position.x, positionObj.transform.position.y, positionObj.transform.position.z - Common.ZOFFSET);
        Utilities.Instance.MoveToWithCallBack(m_CurrentSelected.gameObject, newPos, Common.MOVE_TIME, "OnCardMove");

        m_CurrentSelected.transform.SetParent(positionObj.transform);
    }

    private void GenerateDeck()
    {
        StringBuilder builder = new StringBuilder();
        ushort suitOffset = 12;
        ushort cardOffset = 1;


        for (int i = 0; i < 4; ++i) //Suits
        {
            for (int k = 0; k < 13; ++k) //Card value
            {
                ushort suitValue = (ushort)(1 << (i + suitOffset));
                ushort cardValue = (ushort)((k == 0 ? 0 : 1) << (k - cardOffset));
                builder.Append(Enum.GetName(typeof(Solitaire.SuitType), suitValue));
                builder.Append("_");
                builder.Append(Enum.GetName(typeof(Solitaire.CardValue), cardValue));
                //Debug.Log("cardName = " + builder);

                GameObject card = Instantiate(m_CardPrefab, m_DeckButton.transform.position + Vector3.forward, Quaternion.identity);
                card.transform.SetParent(m_DeckButton.transform);

                m_DeckCards.Add(card.GetComponent<CardElement>());
                m_DeckCards[i * 13 + k].SetCardProperties(this, (ushort)(0 | suitValue | cardValue), builder.ToString());

                builder.Clear();
            }
        }

        Shuffle<CardElement>(m_DeckCards);
    }

    private IEnumerator DealCards()
    {
        int bottomNum = 1;
        int cardNum = 0;
        float yOffset = 0.0f;
        float zOffset = 0.0f;

        for (int i = 0; i < 28; ++i)
        {
            yield return new WaitForSeconds(0.05f);
            if (cardNum == bottomNum)
            {
                bottomNum++;
                cardNum = 0;
                yOffset = 0.0f;
                zOffset = 0.0f;
            }

            m_BottomCards.Add(m_DeckCards[i]);

            iTween.MoveTo(m_BottomCards[i].gameObject, new Vector3(m_BottomList[bottomNum - 1].transform.position.x, m_BottomList[bottomNum - 1].transform.position.y - yOffset, m_BottomList[bottomNum - 1].transform.position.z - 1.0f - zOffset), 0.05f);
            if (cardNum == bottomNum - 1)
            {
                m_BottomCards[i].IsFaceUp = true;
            }
            m_BottomCards[i].position = (Solitaire.CardPosition)(1 << (bottomNum + 13)); //TODO: remove hardcode index
            m_BottomCards[i].transform.SetParent(m_BottomList[bottomNum - 1].transform);

            //Set Previous face down card so we know which card to flip when this card move to other stack
            if(i > 1 && !m_BottomCards[i - 1].IsFaceUp)
            {
                m_BottomCards[i].SetPrevFaceDown(m_BottomCards[i - 1]);
            }

            cardNum++;
            yOffset += Common.YOFFSET;
            zOffset += Common.ZOFFSET;
        }

        m_DeckCards.RemoveRange(0, 28);
    }

    private void OnClickCard(CardElement card)
    {

    }

    private void OnClickButtom(CardElement card)
    {

    }

    private void OnClickTop(CardElement card)
    {

    }

    public void DrawCardFromDeck()
    {
        StartCoroutine(DrawCard());
    }

    public void PutBackToDeck()
    {
        StartCoroutine(PutBackCard());
    }

    private IEnumerator PutBackCard()
    {
        foreach (CardElement card in m_DeckCards)
        {
            iTween.MoveTo(card.gameObject, m_DeckButton.transform.position + Vector3.forward, 0.1f);
            card.position = Solitaire.CardPosition.Deck;
            card.IsFaceUp = false;
        }

        yield break;
    }

    private IEnumerator DrawCard()
    {
        float offSet = 0.5f;

        sbyte currentDrawCard = m_GameData.currentDrawCard;
        if (currentDrawCard > 2)
        {
            for (int i = currentDrawCard - 2; i < currentDrawCard; ++i)
            {
                iTween.MoveTo(m_DeckCards[i].gameObject, new Vector3(m_DeckCards[i].transform.position.x - offSet, m_DeckCards[i].transform.position.y, -(i * offSet)), 0.0f);
            }
        }
        iTween.MoveTo(m_DeckCards[currentDrawCard].gameObject, new Vector3(m_DrawCardHolder.transform.position.x + (currentDrawCard >= 2 ? 1.0f : currentDrawCard * offSet), m_DrawCardHolder.transform.position.y, -(currentDrawCard * offSet)), 0.1f);
        m_DeckCards[currentDrawCard].IsFaceUp = true;
        m_DeckCards[currentDrawCard].position = Solitaire.CardPosition.Draw;

        yield break;
    }

    private void CheckWinCondition()
    {
        if(m_TopCards.Count >= 52)
        {
            m_IsWin = true;
        }
    }

    public bool CheckMoveCard(CardElement cardValue, Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Card":
                {
                    //element.m_CardValue = 1;
                    break;
                }
            case "Bottom":
                {
                    break;
                }
            case "Top":
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        return false;
    }

    public void SetCurrentCard(CardElement card)
    {
        if(m_CurrentSelected && m_CurrentSelected.CardValue != card.CardValue)
        {
            m_CurrentSelected.IsSelected = false;
        }


        m_CurrentSelected = card;
        m_CurrentSelected.IsSelected = true;
    }

    public void Shuffle<T>(List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}

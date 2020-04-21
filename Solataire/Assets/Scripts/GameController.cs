using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private float m_PrevClickedTime;
    private bool m_IsGameStart;
    private int m_MoveRemain;

    private TimeSpan m_ElapsedTime;

    #region Unity functions
    private void OnEnable()
    {
        m_IsGameStart = false;
        EventManager.Instance.Register(Solitaire.Event.OnStartGame, OnStartGame);
        EventManager.Instance.Register(Solitaire.Event.OnNewGame, OnNewGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Solitaire.Event.OnStartGame, OnStartGame);
        EventManager.Instance.Unregister(Solitaire.Event.OnNewGame, OnNewGame);
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {    
        if(!m_IsGameStart)
        {
            return;
        }

        if (m_IsWin)
        {
            Logger.Instance.PrintLog(Common.DEBUG_TAG, "You WIN !!!");
        }

        Vector3 mousePos = Input.mousePosition;

        //Left mouse clicked
        if(Input.GetMouseButtonDown(0))
        {
            Logger.Instance.PrintLog(Common.DEBUG_TAG, "GameController MouseButton down");

            RaycastHit2D hit = Physics2D.Raycast(m_MainCamera.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (hit)
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
                                m_PrevClickedTime = Time.time;
                                break;
                            }

                            //Card in Top position
                            if((card.position & (Solitaire.CardPosition.Top1 | Solitaire.CardPosition.Top2 | Solitaire.CardPosition.Top3 | Solitaire.CardPosition.Top4)) > 0)
                            {
                                if(CanStack(m_CurrentSelected.CardValue, card.CardValue, true) && !m_CurrentSelected.IsInStack())
                                {
                                    StackToCard(card, true);
                                    //TODO: check win condition
                                    CheckWinCondition();
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
                                    m_PrevClickedTime = Time.time;
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
                                    m_PrevClickedTime = Time.time;
                                    return;
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
                                m_PrevClickedTime = 0.0f;
                            }
                            break;
                        }
                }
            }
        }

        //Left mouse held down
        if(Input.GetMouseButton(0))
        {
            Logger.Instance.PrintLog(Common.DEBUG_TAG, "GameController MouseButton drag");
            Logger.Instance.PrintLog(Common.DEBUG_TAG, "time spent = " + (Time.time - m_PrevClickedTime));
            //TODO: more testing to find the magic number :<
            if(m_CurrentSelected && (Time.time - m_PrevClickedTime) >= Common.MIN_DRAGGING_TIME)
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
            Logger.Instance.PrintLog(Common.DEBUG_TAG, "GameController MouseButton up");
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
                                    CheckWinCondition();
                                    return;
                                }
                            }

                            //Card in Bottom position
                            if ((card.position & (Solitaire.CardPosition.Bottom1 | Solitaire.CardPosition.Bottom2 | Solitaire.CardPosition.Bottom3 | Solitaire.CardPosition.Bottom4 | Solitaire.CardPosition.Bottom5 | Solitaire.CardPosition.Bottom6 | Solitaire.CardPosition.Bottom7)) > 0)
                            {
                                CardElement lastCardInStack = card.GetLastCardInStack();
                                if (CanStack(m_CurrentSelected.CardValue, lastCardInStack.CardValue))
                                {
                                    StackToCard(lastCardInStack, false);
                                    return;
                                }
                                //else
                                //{
                                //    m_CurrentSelected.IsSelected = false;
                                //    m_CurrentSelected = card;
                                //    m_CurrentSelected.IsSelected = true;
                                //    return;
                                //}
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

                m_PrevClickedTime = 0.0f;
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

    private void OnApplicationQuit()
    {
        Destroy();
    }
    #endregion

    private void OnNewGame(EventParam param)
    {
        Destroy();

        Utilities.Instance.DispatchEvent(Solitaire.Event.ShowPopup, "newgame", "");
    }

    private void Destroy()
    {
        m_GameData.bottomCards.Clear();
        m_GameData.deckCards.Clear();
        m_GameData.topCards.Clear();
        m_GameData.currentDrawCard = -1;
        m_GameData.difficult = Solitaire.Difficulty.Easy;
        m_GameData.move = 0;
        m_GameData.score = 0;
        m_GameData.time = String.Empty;
        m_GameData.gameMode = Solitaire.GameMode.None;
    }

    private void Initialized()
    {
        m_DeckCards = m_GameData.deckCards = new List<CardElement>();
        m_BottomCards = m_GameData.bottomCards = new List<CardElement>();
        m_TopCards = m_GameData.topCards = new List<CardElement>();
        m_CurrentSelected = null;
        m_IsWin = false;
        m_PrevClickedTime = 0.0f;
        GenerateDeck();
        StartCoroutine(DealCards());

        m_MoveRemain = GameSetting.Instance.difficulty.moveAllowed;
        m_IsGameStart = true;
        m_ElapsedTime = new TimeSpan();
        StartCoroutine(UpdateTime());
    }

    private void OnStartGame(EventParam param)
    {
        Initialized();
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
        if(m_CurrentSelected.IsDragging)
        {
            m_CurrentSelected.EndDragging();
        }

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
                m_CurrentSelected.RemoveCardFromList(ref m_BottomCards);
            }
        }

        if(isStackToTop)
        {
            m_GameData.score += (Common.DEFAULT_SCORE * 2);
            Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "score", m_GameData.score.ToString());
            m_TopCards.Add(m_CurrentSelected);
            iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
            CheckWinCondition();
        }
        else
        {
            m_GameData.score += (Common.DEFAULT_SCORE);
            Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "score", m_GameData.score.ToString());
            if (m_CurrentSelected.position < Solitaire.CardPosition.Bottom1)
            {
                m_CurrentSelected.AddCardToList(ref m_BottomCards);
            }
            //iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3(target.transform.position.x, target.transform.position.y - Common.YOFFSET, target.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
            Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y - Common.YOFFSET, target.transform.position.z - Common.ZOFFSET);
            Utilities.Instance.MoveToWithCallBack(m_CurrentSelected.gameObject, newPos, Common.MOVE_TIME, "OnCardMove");
        }

        m_CurrentSelected.SetCardPosition(target.position);
        m_CurrentSelected.SetCardParent(target.transform.parent);
        //m_CurrentSelected.transform.SetParent(target.transform.parent);
        m_CurrentSelected = null;

        m_GameData.move++;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", m_GameData.move.ToString());
    }

    private void StackToPosition(GameObject positionObj, bool isStackToTop)
    {
        ushort cardPos;
        ushort.TryParse(positionObj.name.Substring(positionObj.name.Length - 1, 1), out cardPos);
        m_CurrentSelected.IsSelected = false;
        if(m_CurrentSelected.IsDragging)
        {
            m_CurrentSelected.EndDragging();
        }
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
                m_CurrentSelected.RemoveCardFromList(ref m_BottomCards);
            }
        }

        if(isStackToTop)
        {
            m_GameData.score += (Common.DEFAULT_SCORE * 2);
            Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "score", m_GameData.score.ToString());
            m_TopCards.Add(m_CurrentSelected);
            m_CurrentSelected.SetCardPosition((Solitaire.CardPosition)(1 << (cardPos + 9)));
        }
        else
        {
            m_GameData.score += (Common.DEFAULT_SCORE);
            Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "score", m_GameData.score.ToString());
            if (m_CurrentSelected.position < Solitaire.CardPosition.Bottom1)
            {
                m_CurrentSelected.AddCardToList(ref m_BottomCards);
            }
            m_CurrentSelected.SetCardPosition((Solitaire.CardPosition)(1 << (cardPos + 13)));
        }

        //iTween.MoveTo(m_CurrentSelected.gameObject, new Vector3(positionObj.transform.position.x, positionObj.transform.position.y, positionObj.transform.position.z - Common.ZOFFSET), Common.MOVE_TIME);
        Vector3 newPos = new Vector3(positionObj.transform.position.x, positionObj.transform.position.y, positionObj.transform.position.z - Common.ZOFFSET);
        Utilities.Instance.MoveToWithCallBack(m_CurrentSelected.gameObject, newPos, Common.MOVE_TIME, "OnCardMove");

        //m_CurrentSelected.transform.SetParent(positionObj.transform);
        //TODO: Thing of better solution man. This is hacky as fuck.
        m_CurrentSelected.SetCardParent(positionObj.transform);
        positionObj.layer = 9;
        m_CurrentSelected = null;

        m_GameData.move++;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", m_GameData.move.ToString());
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
                //Logger.Instance.PrintLog(Common.DEBUG_TAG, "cardName = " + builder);

                GameObject card = Instantiate(m_CardPrefab, m_DeckButton.transform.position + Vector3.forward, Quaternion.identity);
                card.transform.SetParent(m_DeckButton.transform);
                card.layer = 9;

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
                m_BottomList[bottomNum - 1].layer = 9;
                m_BottomCards[i].IsFaceUp = true;
                m_BottomCards[i].gameObject.layer = 8;
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
            card.gameObject.layer = 9;
        }

        m_GameData.move++;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", m_GameData.move.ToString());

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
        m_DeckCards[currentDrawCard].gameObject.layer = 8;

        m_GameData.move++;
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "move", m_GameData.move.ToString());

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

    private EventParam SetupEventParam(int eventId)
    {
        EventParam param = new EventParam();
        param.EventID = eventId;
        //param.Add<string>("score", m_GameData.score.ToString());
        return param;
    }

    private IEnumerator UpdateTime()
    {
        while(!m_IsWin)
        {
            m_ElapsedTime = m_ElapsedTime.Add(TimeSpan.FromSeconds(1));
            if (m_ElapsedTime.TotalHours >= 1)
            {
                m_GameData.time = m_ElapsedTime.ToString(@"hh\:mm\:ss");
            }
            else
            {
                m_GameData.time = m_ElapsedTime.ToString(@"mm\:ss");
            }

            Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "time", m_GameData.time);

            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }
}

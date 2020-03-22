using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]private CommonData m_MainData;
    public List<int> ListCards;

    // Start is called before the first frame update
    private void Start()
    {
        ListCards = new List<int>();
        GenerateDeck();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void GenerateDeck()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int k = 0; k < 13; ++k)
            {
                ListCards.Add(0 | m_MainData.suits[i] | m_MainData.cardValues[k]);

                Debug.Log("Card = " + ListCards[i] + " - Binary = " + Convert.ToString(ListCards[i], 2));
            }
        }
    }
}

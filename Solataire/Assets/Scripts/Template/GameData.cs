using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameDataTemplate",menuName ="Template/GameData")]
public class GameData : ScriptableObject
{
    public List<CardElement> deckCards;
    public List<CardElement> bottomCards;
    public List<CardElement> topCards;
    public sbyte currentDrawCard;

    public Solitaire.GameMode gameMode;
    public Solitaire.Difficulty difficult;
    public ushort score;
    public ushort move;
    public string time;
}

using System;

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

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    public enum GameMode
    {
        None  = 1,
        Klondike,
        Spider,
        FreeCall,
        Pyramid,
        TriPeak
    }

    public enum GameResult
    {
        Still,
        Win,
        Lose
    }

    public enum Event
    {
        OnDataChanged,
        OnLoadingUpdated,
        ShowPopup,
        OnValueChanged,
        OnStartGame,
        OnNewGame,
        PlayEffect,
        PlayAudio,
        OnSettingChanged
    }
}

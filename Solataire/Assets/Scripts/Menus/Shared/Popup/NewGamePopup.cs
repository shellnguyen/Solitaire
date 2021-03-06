﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGamePopup : Popup
{
    [SerializeField] private TextMeshProUGUI m_GameNameText;
    [SerializeField] private Button m_BtnPlay;
    [SerializeField] private ToggleGroup m_DifficultyGroup;
    [SerializeField] private GameData m_GameData;
    [SerializeField] private bool m_IsFirstGame;

    // Start is called before the first frame update
    private void Start()
    {
        m_IsFirstGame = true;
        m_GameData = Resources.FindObjectsOfTypeAll<GameData>().FirstOrDefault();
        m_GameData.gameMode = (Solitaire.GameMode)SceneManager.GetActiveScene().buildIndex;

        m_GameNameText.text = m_GameData.gameMode.ToString();
        m_BtnPlay.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void StartGame()
    {
        foreach(Toggle option in m_DifficultyGroup.ActiveToggles())
        {
            if(option.isOn)
            {
                switch(option.name)
                {
                    case "EasyToggle":
                        {
                            GameSetting.Instance.difficulty = new Easy();
                            break;
                        }
                    case "NormalToggle":
                        {
                            GameSetting.Instance.difficulty = new Normal();
                            break;
                        }
                    case "HardToggle":
                        {
                            GameSetting.Instance.difficulty = new Hard();
                            break;
                        }
                }
            }
        }

        StartCoroutine(ShowVideoAds());
    }

    IEnumerator ShowVideoAds()
    {
        if(GameSetting.Instance.enableAds)
        {
            if (!m_IsFirstGame)
            {
                AdsController.Instance.ShowFullScreen();
            }

            while (AdsController.Instance.IsVideoShowing)
            {
                yield return new WaitForSeconds(2.5f);
            }
        }

        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "game_mode", m_GameData.gameMode.ToString());
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnDataChanged, "difficulty", GameSetting.Instance.difficulty.ToString());
        Utilities.Instance.DispatchEvent(Solitaire.Event.OnStartGame, "start_game", "");
        m_IsFirstGame = false;
        this.gameObject.SetActive(false);

        yield break;
    }
}

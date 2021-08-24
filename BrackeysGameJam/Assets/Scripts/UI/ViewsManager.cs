using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    PlayerSelection,
    InGame,
    Paused
}

public class ViewsManager : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_playerSelect;
    [SerializeField] private GameObject m_gameHUD;
    #endregion

    #region UNITY_REG
    private void Start()
    {
        EnableMainMenuView();
    }
    #endregion

    #region CLASS_REG
    private void EnableView(GameState gameState)
    {
        switch (gameState)
        {
            default:
            case GameState.MainMenu:
                {
                    m_mainMenu.SetActive(true);
                    m_playerSelect.SetActive(false);
                    m_gameHUD.SetActive(false);
                }
                break;
            case GameState.PlayerSelection:
                {
                    m_mainMenu.SetActive(false);
                    m_playerSelect.SetActive(true);
                    m_gameHUD.SetActive(false);
                }
                break;
            case GameState.InGame:
                {
                    m_mainMenu.SetActive(false);
                    m_playerSelect.SetActive(false);
                    m_gameHUD.SetActive(true);
                }
                break;
        }
    }

    public void EnableMainMenuView()
    {
        EnableView(GameState.MainMenu);
    }
    public void EnablePlayerSelectView()
    {
        EnableView(GameState.PlayerSelection);
    }
    public void EnableGameHUDView()
    {
        EnableView(GameState.InGame);
    }
    #endregion 
}

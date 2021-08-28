using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    PlayerSelection,
    InGame,
    GameOver
}

public class ViewsManager : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_playerSelect;
    [SerializeField] private GameHUDView m_gameHUD;
    [SerializeField] private GameObject m_gameOver;

    [SerializeField] private Color m_player01Color;
    [SerializeField] private Color m_player02Color;

    public GameState currentGameState;
    public GameHUDView GameHUD { get => m_gameHUD; }
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
        currentGameState = gameState;

        switch (currentGameState)
        {
            default:
            case GameState.MainMenu:
                {
                    m_mainMenu.SetActive(true);
                    m_playerSelect.SetActive(false);
                    m_gameHUD.gameObject.SetActive(false);
                    m_gameOver.SetActive(false);
                }
                break;
            case GameState.PlayerSelection:
                {
                    m_mainMenu.SetActive(false);
                    m_playerSelect.SetActive(true);
                    m_gameHUD.gameObject.SetActive(false);
                    m_gameOver.SetActive(false);
                }
                break;
            case GameState.InGame:
                {
                    m_mainMenu.SetActive(false);
                    m_playerSelect.SetActive(false);
                    m_gameHUD.gameObject.SetActive(true);
                    m_gameOver.SetActive(false);
                }
                break;
            case GameState.GameOver:
                {
                    m_mainMenu.SetActive(false);
                    m_playerSelect.SetActive(false);
                    m_gameHUD.gameObject.SetActive(false);
                    m_gameOver.SetActive(true);
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
        StartCoroutine(CoEnableGameHUDView());
    }

    private IEnumerator CoEnableGameHUDView()
    {
        yield return new WaitForSeconds(0.3f);

        EnableView(GameState.InGame);
    }

    public void EnableGameOverView(string winner)
    {
        EnableView(GameState.GameOver);
        if (winner.Contains("1"))
            m_gameOver.GetComponent<GameOverView>().winnerText.color = m_player01Color;
        else
            m_gameOver.GetComponent<GameOverView>().winnerText.color = m_player02Color;

        m_gameOver.GetComponent<GameOverView>().winnerText.text = winner;
    }
    #endregion 
}

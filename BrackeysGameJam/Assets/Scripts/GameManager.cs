using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class GameManager : MonoBehaviour// Core.Singleton<GameManager>
{
    [SerializeField] GameConfig m_gameConfig;
    [SerializeField] ViewsManager m_viewsManager;

    int player1Score = 0;
    int player2Score = 0;

    public int Player1Score { get => player1Score;}
    public int Player2Score { get => player2Score;}
    public GameConfig _GameConfig { get => m_gameConfig; }

    private void Start()
    {

    }

    public void PlayerDead(PlayerController controller)
    {
        if (controller.gameObject.tag == Constants.TAG_PLAYER_1)
        {
            player2Score++;
        }
        if (controller.gameObject.tag == Constants.TAG_PLAYER_2)
        {
            player1Score++;
        }

        m_viewsManager.GameHUD.Refresh();

        if (player1Score == m_gameConfig.WIN_SCORE)
        {
            OnGameWin();
            Debug.LogError("A");
        }

        if (player2Score == m_gameConfig.WIN_SCORE)
        {
            OnGameWin();
            Debug.LogError("B");
        }


    }

    public void OnGameWin()
    {

    }

}

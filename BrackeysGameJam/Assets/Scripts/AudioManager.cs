using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region VARIABLE_REG
    public AudioClip mainMenuClip;
    public AudioClip ingameClip;
    public AudioClip victoryClip;

    private AudioSource audioSource;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    #endregion

    #region CLASS_REG
    public void ChangeTrack(GameState currentGameState)
    {
        switch (currentGameState)
        {
            default:
            case GameState.MainMenu:
                {
                    ChangeBGM(mainMenuClip);
                }
                break;
            case GameState.PlayerSelection:
                {
                    ChangeBGM(mainMenuClip);
                }
                break;
            case GameState.InGame:
                {
                    ChangeBGM(ingameClip);
                }
                break;
            case GameState.GameOver:
                {
                    ChangeBGM(victoryClip, false);
                }
                break;
        }
    }

    private void ChangeBGM(AudioClip music, bool loop = true)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.loop = loop;
        audioSource.Play();
    }
    #endregion
}

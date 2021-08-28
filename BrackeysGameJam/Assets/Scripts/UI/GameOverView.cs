using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private Button m_primarySelectedButton;

    public TextMeshProUGUI winnerText;
    #endregion

    #region UNITY_REG
    private void Start()
    {
        Init();
    }
    #endregion

    #region CLASS_REG
    private void Init()
    {
        m_primarySelectedButton.Select();
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene("Demo");
    }

    public void OnExitPressed()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private Button m_primarySelectedButton;

    public UnityEvent Event_OnPlay;
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

    public void OnPlayPressed()
    {
        Event_OnPlay.Invoke();
        Debug.Log("Play");
    }

    public void OnExitPressed()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    #endregion 
}

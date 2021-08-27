using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    #region VARIABLE_REG
    private PlayerController m_pController;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_pController = FindObjectOfType<PlayerController>();
    }
    #endregion

    #region CLASS_REG
    public void EnableMovement()
    {
        m_pController.CanMove = true;
    }

    public void DisableMovement()
    {
        m_pController.CanMove = false;
    }
    #endregion 
}

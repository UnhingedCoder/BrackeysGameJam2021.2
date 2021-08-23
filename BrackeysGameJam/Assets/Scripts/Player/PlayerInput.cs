using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region VARIABLE_REG
    private Vector2 m_moveDir;
    private Vector2 m_aimDir;
    private Vector2 m_mousePos;


    public Vector2 moveDirection { get => m_moveDir; }
    public Vector2 aimDirection { get => m_aimDir; }
    public Vector2 mousePos { get => m_mousePos; }

    private PlayerControls m_playerControls;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        m_playerControls.Enable();
    }

    private void Update()
    {
        ReadInputs();
    }

    private void OnDisable()
    {
        m_playerControls.Disable();
    }
    #endregion

    #region CLASS_REG

    private void ReadInputs()
    {
        m_moveDir = m_playerControls.Player.Move.ReadValue<Vector2>();

        m_aimDir = m_playerControls.Player.Aim.ReadValue<Vector2>();

        m_mousePos = m_playerControls.Player.MouseAim.ReadValue<Vector2>();
    }
    #endregion 
}

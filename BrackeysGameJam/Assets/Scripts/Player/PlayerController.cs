using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region VARIABLE_REG
    [Header("INPUT")]

    [Header("MOVEMENT")]
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_turnSpeed = 0.1f;

    public bool CanMove = true;
    private bool m_aimWithController = false;

    private Transform m_playerModel;
    private Camera m_cam;

    private Vector3 m_moveDirection;

    private Vector2 m_moveInput;
    private Vector2 m_gamepadAim;
    private Vector2 m_mouseAim;

    private CharacterController m_controller;
    private PlayerInput m_pInput;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_playerModel = this.transform.GetChild(0);
        m_cam = Camera.main;

        m_controller = this.GetComponent<CharacterController>();
        m_pInput = this.GetComponent<PlayerInput>();
    }

    private void Start()
    {
        SetControlScheme();
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region CLASS_REG
    public void OnMove(InputAction.CallbackContext context)
    {
        m_moveInput = context.ReadValue<Vector2>();
    }

    public void OnGamepadAim(InputAction.CallbackContext context)
    {
        m_gamepadAim = context.ReadValue<Vector2>();
    }

    public void OnMouseAim(InputAction.CallbackContext context)
    {
        m_mouseAim = context.ReadValue<Vector2>();
    }

    private void SetControlScheme()
    {
        if (m_pInput.currentControlScheme == "KeyboardMouse")
            m_aimWithController = false;
        else
            m_aimWithController = true;

        //CanMove = false;
    }

    private void Move()
    {
        if (!CanMove)
        {
            m_controller.Move(Vector3.zero);
            return;
        }

        //Movement
        float yDir = m_moveDirection.y;
        m_moveDirection = (this.transform.forward * m_moveInput.y) + (this.transform.right * m_moveInput.x);
        m_moveDirection = m_moveDirection.normalized * m_moveSpeed;
        m_moveDirection.y = yDir;

        m_controller.Move(m_moveDirection * Time.deltaTime);

        //Rotation
        if (m_aimWithController)
        {
            Vector3 playerDirection = Vector3.right * m_gamepadAim.x + Vector3.forward * m_gamepadAim.y;
            if (playerDirection.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        else
        {
            Ray cameraRay = m_cam.ScreenPointToRay(m_mouseAim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
    #endregion
}

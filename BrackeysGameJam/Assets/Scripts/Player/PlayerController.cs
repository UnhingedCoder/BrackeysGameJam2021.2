using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region VARIABLE_REG
    [Header("MOVEMENT")]
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_turnSpeed = 0.1f;

    public bool CanMove = true;
    public bool AimWithController = false;

    private Transform m_playerModel;
    private Camera m_cam;

    private Vector3 m_moveDirection;

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
    private void Update()
    {
        Move();
    }
    #endregion

    #region CLASS_REG
    private void Move()
    {
        if (!CanMove)
        {
            m_controller.Move(Vector3.zero);
            return;
        }

        //Movement
        float yDir = m_moveDirection.y;
        m_moveDirection = (this.transform.forward * m_pInput.moveDirection.y) + (this.transform.right * m_pInput.moveDirection.x);
        m_moveDirection = m_moveDirection.normalized * m_moveSpeed;
        m_moveDirection.y = yDir;

        m_controller.Move(m_moveDirection * Time.deltaTime);

        //Rotation
        if (AimWithController)
        {
            Vector3 playerDirection = Vector3.right * m_pInput.aimDirection.x + Vector3.forward * m_pInput.aimDirection.y;
            if (playerDirection.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        else
        {
            Ray cameraRay = m_cam.ScreenPointToRay(m_pInput.mousePos);
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

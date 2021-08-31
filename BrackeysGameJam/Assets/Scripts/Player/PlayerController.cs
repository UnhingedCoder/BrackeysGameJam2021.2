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

    [Header("PROJECTILES")]
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private GameObject m_boomerangBulletPrefab;
    [SerializeField] private GameObject m_bulletSpawnPoint;
    [SerializeField]  GameObject shieldObject;

    [SerializeField] private float m_fireRate;

    [Header("INVULNERABILTY")]
    [SerializeField] private float m_MaxInvulnerabiltyTime;
    [SerializeField] private float m_invulnerableFXEmissionRate;
    [SerializeField] private ParticleSystem m_invulnerableFX;

    [SerializeField] protected float MaxBulletSpawn; // remove from here and put in Gamemanager?
    private int bulletCount = 0;
    private GameObject boomerangObject;
    private bool isShieldCharged = true;

    public bool CanMove = true;
    private bool m_aimWithController = false;
    private bool m_isInvulnerable = false;
    private bool m_canFire = true;
    private float lastFired;

    private Transform m_playerModel;
    private Camera m_cam;

    private Vector3 m_spawnPoint;
    private Vector3 m_moveDirection;

    private Vector2 m_moveInput;
    private Vector2 m_gamepadAim;
    private Vector2 m_mouseAim;
    private Vector2 m_primaryFire;

    private CharacterController m_controller;
    private PlayerInput m_pInput;
    private PlayerHealth m_pHealth;
    [SerializeField] private Animator m_anim;
    private AudioSource m_audioSource;

    private GameManager m_gameManager;

    public Vector2 MouseAim { get => m_mouseAim; }
    public GameManager m_GameManager { get => m_gameManager; }
    public GameObject ShieldObject { get => shieldObject; }
    public GameObject BulletSpawnPoint { get => m_bulletSpawnPoint; }

    //  public PlayerHealth PHealth { get => m_pHealth; }

    private float m_invulnerabilityTimer = 0f;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();

        m_playerModel = this.transform.GetChild(0);
        m_cam = Camera.main;

        m_controller = this.GetComponent<CharacterController>();
        m_pInput = this.GetComponent<PlayerInput>();
        m_pHealth = new PlayerHealth(this);
        m_audioSource = this.GetComponent<AudioSource>();

        m_gameManager.Event_Winner.AddListener(ShowEndGameAnimation);
    }

    private void Start()
    {
        m_spawnPoint = this.transform.position;
        SetControlScheme();
    }

    private void Update()
    {
        Move();
        HandleInvulnerability();

        if (!m_canFire && (Time.realtimeSinceStartup - lastFired > m_fireRate))
        {
            m_canFire = true;
        }



        OnShieldOn();
        RestoreShield();
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

    public void OnFire1(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.scene.IsValid())
        {
            if (m_canFire)
            {
                if (!m_gameManager.CanGameStart())
                    return;

                lastFired = Time.realtimeSinceStartup;
                m_canFire = false;

                if (bulletCount < MaxBulletSpawn)
                {
                    bulletCount++;
                    var bullet = Instantiate(m_bulletPrefab, this.m_bulletSpawnPoint.transform.position, this.gameObject.transform.rotation);
                    bullet.GetComponent<BulletProjectile>().AssignController(this);
                }
            }
        }
        
    }

    public void OnFire2(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.scene.IsValid())
        {
            if (!m_gameManager.CanGameStart())
                return;

            if (boomerangObject == null)
            {
                boomerangObject = Instantiate(m_boomerangBulletPrefab, this.m_bulletSpawnPoint.transform.position, this.gameObject.transform.rotation);
                boomerangObject.GetComponent<BoomerangProjectile>().AssignController(this);
            }
            else
            {
                boomerangObject.GetComponent<BoomerangProjectile>().ReturnToPlayer();
            }
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.scene.IsValid())
        {
            if (!m_gameManager.CanGameStart())
                return;

            if (isShieldCharged)
            {
                SwitchShieldOn();
            }
        }
    }

    float shield_currentTimer;
    public float shield_maxTimer = 3;

    float shield_restoreTimer;
    public float shield_restoreMaxTimer = 5;

    bool isShieldOn = false;

    void SwitchShieldOn()
    {
        shield_currentTimer = 0f;
        shield_restoreTimer = 0f;
        isShieldCharged = false;
        isShieldOn = true;
        shieldObject.gameObject.SetActive(true); 
    }

    void OnShieldOn()
    {
        if (isShieldOn)
        {
            if(shield_currentTimer <= shield_maxTimer)
            {
                shield_currentTimer += Time.deltaTime;
            }
            else
            {
                shieldObject.gameObject.SetActive(false);
                shield_currentTimer = 0;
                isShieldOn = false;
            }
        }
    }

    void RestoreShield()
    {
        if (!isShieldOn && !isShieldCharged)
        {
            if (shield_restoreTimer <= shield_restoreMaxTimer)
            {
                shield_restoreTimer += Time.deltaTime;
            }
            else
            {
                shield_restoreTimer = 0;
                isShieldCharged = true;
            }
        }
    }

    public float GetShieldChargePercentage()
    {
        if (isShieldCharged)
        {
            return 1;
        }
        return shield_restoreTimer / shield_restoreMaxTimer;
    }

    public void OnBulletDestroyed()
    {
        bulletCount--;
    }

    public void OnPlayerHit(int dmg, bool isLava = false)
    {
        if (m_isInvulnerable)
            return;

        CanMove = false;

        m_audioSource.Play();
        m_pHealth.OnPlayerHit(dmg);
        m_anim.SetTrigger("Hit");
        m_invulnerabilityTimer = 0f;
        m_isInvulnerable = true;

        if (isLava)
        {
            Debug.Log("Resetting position");
            this.transform.position = m_spawnPoint;
        }
    }

    public float CurrentHealthPercentage()
    {
        return (float)m_pHealth.CurrentHP / (float)m_pHealth.MaxHP;
    }

    public void PlayerDead()
    {
        m_gameManager.PlayerDead(this);
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        this.transform.position = m_spawnPoint;
        m_pHealth.ResetHP();
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
        if (!m_gameManager.CanGameStart())
            return;

        if (!CanMove)
            return;


        //Rotation 
        Vector3 playerDirection = Vector3.zero; 
        if (m_aimWithController)
        {
            playerDirection = Vector3.right * m_gamepadAim.x + Vector3.forward * m_gamepadAim.y;
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
                playerDirection = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
                transform.LookAt(playerDirection);
            }
        }

        //Movement
        float yDir = m_moveDirection.y;
        m_moveDirection = (Vector3.forward * m_moveInput.y) + (Vector3.right * m_moveInput.x);
        m_moveDirection = m_moveDirection.normalized * m_moveSpeed;
        m_moveDirection.y = yDir;
        m_moveDirection.y += (Physics.gravity.y * Time.deltaTime);

        m_controller.Move(m_moveDirection * Time.deltaTime);
    }

    private void UpdateMoveAnimation()
    {
        float magnitude = 0;
        if(m_moveInput.x != 0 || m_moveInput.y != 0)
            magnitude = 1;

        m_anim.SetFloat("Move", magnitude);
    }

    private void ShowEndGameAnimation(string winnerTag)
    {
        if (this.gameObject.CompareTag(winnerTag))
        {
            m_anim.SetTrigger("Win");
        }
        else
        {
            m_anim.SetTrigger("Lose");
        }
    }

    private void HandleInvulnerableFX(float rate)
    {
        var emission = m_invulnerableFX.emission;
        emission.rateOverTime = rate;
    }

    private void HandleInvulnerability()
    {
        if (m_isInvulnerable)
        {
            if (m_invulnerabilityTimer <= m_MaxInvulnerabiltyTime)
            {
                HandleInvulnerableFX(m_invulnerableFXEmissionRate);
                m_invulnerabilityTimer += Time.deltaTime;
            }
            else
            {
                HandleInvulnerableFX(0f);
                m_isInvulnerable = false;
            }
        }
    }
    #endregion
}

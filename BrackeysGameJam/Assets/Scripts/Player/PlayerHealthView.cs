using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private GameObject m_healthContainer;
    [SerializeField] private Image m_healthBar;
    [SerializeField] private PlayerController m_pController;
    [SerializeField] private Gradient m_healthBarGradient;

    [SerializeField] private ViewsManager m_viewsManager;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_viewsManager = FindObjectOfType<ViewsManager>();    
    }

    private void OnEnable()
    {
        m_pController = this.transform.root.GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateHealth();
    }
    #endregion

    #region CLASS_REG

    void UpdateHealth()
    {
        bool state = m_viewsManager.currentGameState == GameState.InGame ? true : false;
        m_healthContainer.SetActive(state);

        m_healthBar.fillAmount = m_pController.CurrentHealthPercentage();
        m_healthBar.color = m_healthBarGradient.Evaluate(m_pController.CurrentHealthPercentage());
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private Image m_healthBar;
    [SerializeField] private PlayerController m_pController;
    #endregion

    #region UNITY_REG
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
        m_healthBar.fillAmount = m_pController.CurrentHealthPercentage();
    }
    #endregion
}

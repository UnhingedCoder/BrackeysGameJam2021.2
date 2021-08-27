using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentHP;

    private int maxHP;

    PlayerController m_controller;

    public PlayerHealth(PlayerController controller)
    {
        m_controller = controller;

        maxHP = m_controller.m_GameManager._GameConfig.MAX_HP;
        currentHP = maxHP;

    }

    public void OnPlayerHit(int damage)
    {
        currentHP -= damage;
        Debug.LogError(currentHP);
        if (currentHP <= 0)
        {
            Debug.LogError("DEAD");
            m_controller.PlayerDead();
        }
    }

    public void OnPlayerRegen(int hpRestored)
    {

    }

    public void ResetHP()
    {
        currentHP = maxHP;
    }
}

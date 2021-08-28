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
        currentHP = MaxHP;

    }

    public int CurrentHP { get => currentHP; }
    public int MaxHP { get => maxHP; }

    public void OnPlayerHit(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            m_controller.PlayerDead();
        }
    }

    public void OnPlayerRegen(int hpRestored)
    {

    }

    public void ResetHP()
    {
        currentHP = MaxHP;
    }
}

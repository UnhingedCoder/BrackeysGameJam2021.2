using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentHP;

    private int maxHP = 100; //remove from here and put in Gamemanager?

    PlayerController m_controller;
    public PlayerHealth(PlayerController controller)
    {
        m_controller = controller;
        currentHP = maxHP;
    }

    public void OnPlayerHit(int damage)
    {
        currentHP -= damage;
        Debug.LogError(currentHP);
        if (currentHP <= 0)
        {
            Debug.LogError("DEAD");
          //  GameManager.Instance.PlayerDead();
        //  Events.PlayerDead
        }
    }

    public void OnPlayerRegen(int hpRestored)
    {

    }
}

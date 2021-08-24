using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterSwitcher : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private List<GameObject> m_characters = new List<GameObject>();

    public UnityEvent Event_OnMaxPlayerLimit;

    private PlayerInputManager m_manager;

    int index = 0;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_manager = this.GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        index = Random.Range(0, m_characters.Count);
        m_manager.playerPrefab = m_characters[index];
    }
    #endregion

    #region CLASS_REG
    public void HandlePlayerJoining(bool state)
    {
        if (m_manager == null)
            return;

        if (state)
            m_manager.EnableJoining();
        else
            m_manager.DisableJoining();
    }

    public void SwitchNextSpawnCharacter()
    {
        if (index < m_characters.Count - 1)
            index++;
        else
            index = 0;

        m_manager.playerPrefab = m_characters[index];

        if (MaxPlayerLimitReached())
        {
            Event_OnMaxPlayerLimit.Invoke();
        }
    }

    public bool MaxPlayerLimitReached()
    {
        return m_manager.playerCount >= m_manager.maxPlayerCount ? true : false;

    }
    #endregion
}

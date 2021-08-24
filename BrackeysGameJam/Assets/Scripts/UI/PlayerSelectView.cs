using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerSelectView : MonoBehaviour
{
    #region VARIABLE_REG
    public UnityEvent Event_AllowPlayerJoining;
    #endregion

    #region UNITY_REG
    private void OnEnable()
    {
        Event_AllowPlayerJoining.Invoke();
    }
    #endregion

    #region CLASS_REG
    #endregion 
}

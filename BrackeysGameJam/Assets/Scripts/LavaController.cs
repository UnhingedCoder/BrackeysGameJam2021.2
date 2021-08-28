using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] protected int Damage;
    #endregion

    #region UNITY_REG
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            StartCoroutine(CoTakeLavaDamage(other.gameObject.GetComponent<PlayerController>()));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().OnPlayerHit(Damage, true);
        }
    }
    #endregion

    #region CLASS_REG
    private IEnumerator CoTakeLavaDamage(PlayerController _player)
    {
        yield return new WaitForSeconds(1f);

        _player.OnPlayerHit(Damage, true);
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Bounces;

    Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = this.gameObject.GetComponent<Rigidbody>();

       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError("SPACE");
            m_rigidbody.AddForce(transform.forward * Speed);
        }

       // this.transform.Translate(Vector3.one.normalized * Speed * Time.deltaTime);
    }

}

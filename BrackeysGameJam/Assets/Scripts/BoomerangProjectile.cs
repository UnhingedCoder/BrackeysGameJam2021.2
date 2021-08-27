using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectiles
{
    bool m_onReturn = false;
    bool m_returnable = false;

    private void FixedUpdate()
    {
        if (m_onReturn)
        {
            // transform.LookAt(controller.gameObject.transform);


            float step = 20 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, controller.gameObject.transform.position, step);

            
            if (Vector3.Distance(transform.position, controller.gameObject.transform.position) < 0.001f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            m_rigidbody.velocity = Vector3.zero;
            m_returnable = true;
        }

        if (collision.gameObject.tag != controller.gameObject.tag)
        {
            collision.gameObject.GetComponent<PlayerController>().PHealth.OnPlayerHit(Damage);
        }
    }

    public void ReturnToPlayer()
    {
        if (m_returnable) { 
            m_onReturn = true;
            // m_rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
        }
    }
}

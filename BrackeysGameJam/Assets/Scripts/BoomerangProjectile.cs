using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(controller.transform.position.x, controller.BulletSpawnPoint.transform.position.y,controller.transform.position.z), step);

            Debug.LogError(Vector3.Distance(transform.position, controller.gameObject.transform.position));
            if (Vector3.Distance(transform.position, controller.gameObject.transform.position) < 0.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void AssignController(PlayerController controller_)
    {
        controller = controller_;
        Physics.IgnoreCollision( controller_.ShieldObject.GetComponent<Collider>(), this.GetComponent<Collider>(),true);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("Collision : " + collision.gameObject.name);
        if (collision.gameObject.tag == "Walls")
        {
            m_rigidbody.velocity = Vector3.zero;
            m_returnable = true;
        }

        if (collision.gameObject.tag.Contains("Player") && collision.gameObject.tag != controller.gameObject.tag)
        {
            collision.gameObject.GetComponent<PlayerController>().OnPlayerHit(Damage);
        }

        //if (collision.gameObject.tag.Contains("Shield"))
        //{
        //    Destroy(this.gameObject);
        //}
    }

    public void ReturnToPlayer()
    {
        if (m_returnable) { 
            m_onReturn = true;
            // m_rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
        }
    }
}

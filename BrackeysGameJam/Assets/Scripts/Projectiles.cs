using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Bounces;

    public GameObject burstFX;

    protected Rigidbody m_rigidbody;

    protected int bounceCount = 0;

    public PlayerController controller;

    private void Start()
    {
        m_rigidbody = this.gameObject.GetComponent<Rigidbody>();

        FireBullet();
    }

    private void FixedUpdate()
    {
        
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            bounceCount++;
            if (bounceCount >= Bounces)
            {
                Instantiate(burstFX, this.transform.position, Quaternion.identity);
                controller.OnBulletDestroyed();
                Destroy(this.gameObject);
            }
        }

        if (collision.gameObject.tag.Contains("Player"))
        {
            Instantiate(burstFX, this.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<PlayerController>().OnPlayerHit(Damage);

        }
    }

    public void FireBullet()
    {
        m_rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
    }

}

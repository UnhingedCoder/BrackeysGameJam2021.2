using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectiles : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Bounces;

    public GameObject burstFX;

    protected Rigidbody m_rigidbody;

    protected int bounceCount = 0;

    protected PlayerController controller;

    private void Awake()
    {
 
    }

    private void Start()
    {
        m_rigidbody = this.gameObject.GetComponent<Rigidbody>();

        FireBullet();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(this.transform.position, controller.transform.position) > 30){
            DestroyProjectile();
        }
    }

    public abstract void AssignController(PlayerController controller_);

    public virtual void OnCollisionEnter(Collision collision)
    {
        Instantiate(burstFX, this.transform.position, Quaternion.identity);

        if (collision.gameObject.tag == "Walls")
        {
            bounceCount++;
            if (bounceCount >= Bounces)
            {
                DestroyProjectile();
            }
        }

        if (collision.gameObject.tag.Contains("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().OnPlayerHit(Damage);
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        // Instantiate(burstFX, this.transform.position, Quaternion.identity);
        controller.OnBulletDestroyed();
        Destroy(this.gameObject);
    }

    public void FireBullet()
    {
        m_rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
    }

}

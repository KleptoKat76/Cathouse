using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //Settable Variables
    public float m_speed;
    public float m_dieTime;

    //Physics Calculation
    private Rigidbody2D rb;
    private Vector2 m_dir;

    //Ownership
    private PlayerGameState.PlayerID owner;
    private bool reflected;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * m_speed;
    }
    private void FixedUpdate()
    {
        //rb.velocity = transform.up * m_speed;
        m_dieTime -= Time.deltaTime;
        if(m_dieTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void reflectBullet()
    {
        rb.velocity *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ANDREW 
        if (collision.gameObject.CompareTag("Reflective"))
        {
            Vector2 wallNormal = collision.GetContact(0).normal;
            m_dir = Vector2.Reflect(rb.velocity, wallNormal).normalized;
            transform.up = m_dir;
            rb.velocity = m_dir * m_speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BulletReflect" && !reflected)
        {
            reflectBullet();
            reflected = true;
        }
    }
}

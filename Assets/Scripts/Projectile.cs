using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Settable Variables
    public float m_speed;
    public float m_dieTime;

    //Physics Calculation
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * m_speed;
        m_dieTime -= Time.deltaTime;
        if(m_dieTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

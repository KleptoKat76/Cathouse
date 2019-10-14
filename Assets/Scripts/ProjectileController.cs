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

    //Ownership
    private PlayerController.PlayerID owner;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ANDREW 
    }
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
            Destroy(this.gameObject);
        }
    }
}

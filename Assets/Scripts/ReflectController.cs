using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectController : MonoBehaviour
{
    PlayerController playerController;
    public float m_dieTime;
    public LayerMask bullet;
    public bool farReflect;
    public bool mediumReflect;
    public bool closeReflect;
    private float farReflectDistance = 2.0f;
    private float mediumReflectDistance = 1.0f;
    private float closeReflectDistance = .5f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<PlayerController>();
        if (Physics2D.OverlapCircle(transform.position, closeReflectDistance, bullet))
        {
            closeReflect = true;
        }
        else if (Physics2D.OverlapCircle(transform.position, mediumReflectDistance, bullet))
        {
            mediumReflect = true;
        }
        else if (Physics2D.OverlapCircle(transform.position, farReflectDistance, bullet))
        {
            farReflect = true;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        m_dieTime -= Time.deltaTime;
        if (m_dieTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

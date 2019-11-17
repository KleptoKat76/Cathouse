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
    public Distance dist = Distance.notHit;
    private static readonly float farReflectDistance = 2.0f;
    private static readonly float mediumReflectDistance = 1.0f;
    private static readonly float closeReflectDistance = .5f;

    public enum Distance
    {
        notHit, close, medium, far
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<PlayerController>();
        if (Physics2D.OverlapCircle(transform.position, closeReflectDistance, bullet))
        {
            dist = Distance.close;
        }
        else if (Physics2D.OverlapCircle(transform.position, mediumReflectDistance, bullet))
        {
            dist = Distance.medium;
        }
        else if (Physics2D.OverlapCircle(transform.position, farReflectDistance, bullet))
        {
            dist = Distance.far;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectController : MonoBehaviour
{
    public float m_dieTime;
    // Start is called before the first frame update
    void Start()
    {
        
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

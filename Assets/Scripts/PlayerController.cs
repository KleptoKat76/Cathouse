using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerSpeed;
    public GameObject projectile;
    private GunController gun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerMovement();
        checkShoot(projectile);
    }

    public void checkPlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-playerSpeed, rb.velocity.y, 0);
        }  
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, playerSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, -playerSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(playerSpeed, rb.velocity.y, 0);
        }
 
    }

    public void checkShoot(GameObject bulletType)
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot(bulletType);
        }
    }

}





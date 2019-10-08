using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Side Movement
    public float playerSpeed;
    private Rigidbody2D rb;
    public float maxSpeedX;
    //Jump variables
    public float jumpForce;
    public LayerMask ground;
    private bool grounded;
    private GameObject groundCheck;
    //Fast Fall variable
    public float fastFallMultiplier;
    //Shooting variables
    public GameObject projectile;
    private GunController gun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunController>();
        foreach(Transform child in transform)
        {
            if(child.gameObject.name == "GroundCheck")
            {
                groundCheck = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerMovement();
        checkShoot(projectile);
    }

    public void checkPlayerMovement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .1f, ground);
        //Walk Left
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-playerSpeed * transform.right);
            if(rb.velocity.x < -maxSpeedX)
            {
                rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
            }
        }
        //Walk Right
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(playerSpeed * transform.right);
            if (rb.velocity.x > maxSpeedX)
            {
                rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
            }
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        //Smaller Jump
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            }
        }
        //Fast Fall
        if (Input.GetKeyDown(KeyCode.S) && !grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, fastFallMultiplier * jumpForce) * -transform.up;
        }
 
    }

    public void checkShoot(GameObject bulletType)
    {
        if (Input.GetMouseButton(0))
        {
            gun.Shoot(bulletType);
        }
    }
    public void doReflect()
    {
        //PATRICK
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controller
    public Controller controller;
    private float parryTime = 1.0f;
    private ControlScheme cntrlSchm;
    //Side Movement
    public float playerSpeed;
    private Rigidbody2D rb;
    public float maxSpeedX;
    public float maxSpeedY;
    //Jump variables
    public float jumpForce;
    public LayerMask ground;
    private bool grounded;
    private GameObject groundCheck;
    //Fast Fall variable
    public float fastFallMultiplier;
    //Shooting variables
    private GunController gun;

    
    public enum Controller
    {
        contr0, contr1, contr2, contr3, keyboard
    }
    // Start is called before the first frame update
    void Start()
    {
        cntrlSchm = new ControlScheme(controller);
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
        checkShoot();

    }
    private void FixedUpdate()
    {
        checkPlayerMovement();
        doReflect();
    }
    public void checkPlayerMovement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .2f, ground);
        float horizontalInput = Input.GetAxis(cntrlSchm.HorizontalAxis);
        float verticalInput = Input.GetAxis(cntrlSchm.VerticalAxis);
        //Walk 
        rb.AddForce(playerSpeed * horizontalInput * transform.right);
        if(Mathf.Abs(rb.velocity.x) > maxSpeedX)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
        //Jump
        if (Input.GetAxis(cntrlSchm.JumpAxis) > 0 && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (rb.velocity.y > maxSpeedY)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.1f);
            }
        }
        //Smaller Jump
        else if (Input.GetAxis(cntrlSchm.JumpAxis) <= 0 && !grounded)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            }
        }
        //Fast Fall
        if (verticalInput < 0 && !grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, fastFallMultiplier * jumpForce) * -transform.up;
        }
 
    }

    public void checkShoot()
    {
        if (Input.GetMouseButton(0))
        {
            gun.Shoot();
        }
    }

}





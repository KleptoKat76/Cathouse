using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controller
    public Controller controller;
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
    private bool jumpKeyUp;
    //Fast Fall variable
    public float fastFallMultiplier;
    //Shooting variables
    private GunController gun;
    //Sprite Facing
    private Direction dir;
    private SpriteRenderer sprtRend;
    public enum Controller
    {
        contr0, contr1, contr2, contr3, keyboard
    }
    public enum Direction
    {
        left, right
    }
    // Start is called before the first frame update
    void Start()
    {
        jumpKeyUp = true;
        dir = Direction.left;
        sprtRend = GetComponent<SpriteRenderer>();
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
    }
    public void checkPlayerMovement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .4f, ground);
        float horizontalInput = Input.GetAxis(cntrlSchm.HorizontalAxis);
        float verticalInput = Input.GetAxis(cntrlSchm.VerticalAxis);
        //Sprite Flip
        if(horizontalInput < 0 && dir == Direction.right)
        {
            dir = Direction.left;
            flipSprite();
        }
        else if(horizontalInput > 0 && dir == Direction.left)
        {
            dir = Direction.right;
            flipSprite();
        }
        //Walk 
        rb.AddForce(playerSpeed * horizontalInput * transform.right);
        if(Mathf.Abs(rb.velocity.x) > maxSpeedX)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
        //Jump
        if (Input.GetAxisRaw(cntrlSchm.JumpAxis) > 0 && grounded)
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
    public void doReflect()
    {
        //PATRICK
    }
    private void flipSprite()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
  
}





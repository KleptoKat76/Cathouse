using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player ID
    private PlayerGameState.PlayerID playerID;
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
    private bool jumpKeyUp;
    //Fast Fall variable
    public float fastFallMultiplier;
    //Shooting variables
    private GunController gun;
    //Sprite Facing
    private Direction dir;
    private SpriteRenderer sprtRend;
    //Reflect variables
    private bool currentlyReflecting = false;
    private bool canReflect = true;
    private float reflectTimer;
    public float reflectCooldown;
    public float reflectDuration;

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
        reflectInput();
    }
    private void FixedUpdate()
    {
        checkPlayerMovement();
        reflectTimer -= Time.deltaTime;
        checkReflectCooldown();
    }
    public void checkPlayerMovement()
    {
        //Ground stuff
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .4f, ground);
        //Horiz. vert. input 
        float horizontalInput = Input.GetAxis(cntrlSchm.HorizontalAxis);
        float verticalInput = Input.GetAxis(cntrlSchm.VerticalAxis);
        //Sprite Flip
        if(horizontalInput < 0 && dir == Direction.right)
        {
            dir = Direction.left;
            sprtRend.flipX = !sprtRend.flipX;
        }
        else if(horizontalInput > 0 && dir == Direction.left)
        {
            dir = Direction.right;
            sprtRend.flipX = !sprtRend.flipX;
        }
        //Walk 
        rb.AddForce(playerSpeed * horizontalInput * transform.right);
        if(Mathf.Abs(rb.velocity.x) > maxSpeedX)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
        //Jump
        if (Input.GetAxis(cntrlSchm.JumpAxis) > 0 && grounded && jumpKeyUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (rb.velocity.y > maxSpeedY)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.1f);
            }
            jumpKeyUp = false;
        }
        //Smaller Jump
        else if (Input.GetAxis(cntrlSchm.JumpAxis) <= 0 && !grounded)
        {
            jumpKeyUp = true;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            }
        }
        if(Input.GetAxis(cntrlSchm.JumpAxis) <= 0)
        {
            jumpKeyUp = true;
        }
        //Fast Fall
        if (verticalInput < 0 && !grounded)
        {
            rb.AddForce(-transform.up * fastFallMultiplier * 3, ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x, fastFallMultiplier * jumpForce) * -transform.up;
        }
 
    }

    public void checkShoot()
    {
        if (Input.GetAxis(cntrlSchm.ShootAxis) > 0)
        {
            gun.Shoot();
        }
    }
    //Reflect
    private void reflectInput()
    {
        if (Input.GetAxis(cntrlSchm.ReflectAxis) > 0)
        {
            if (canReflect == true)
            {
                currentlyReflecting = true;
                canReflect = false;
                reflectTimer = reflectCooldown;
                StartCoroutine(waitReflect(reflectDuration));
            }
        }
    }

    private void checkReflectCooldown()
    {
        if (reflectTimer < 0)
        {
            canReflect = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetType() == typeof(CapsuleCollider2D) && 
            collision.gameObject.CompareTag("Bullet"))
        {
            var currentController = collision.gameObject.GetComponent<ProjectileController>();
            currentController.ReflectBullet();
        }
    }

    private IEnumerator waitReflect(float reflectTime)
    {
        yield return new WaitForSeconds(reflectTime);
        currentlyReflecting = false;
    }

    public ControlScheme getControlScheme()
    {
        return new ControlScheme(controller);
    }
  
}





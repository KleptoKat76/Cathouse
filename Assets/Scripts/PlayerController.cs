﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player ID
    private PlayerID playerID = PlayerID.p1;
    private State state = State.alive;
    //Controller
    public ControlScheme.Controller controller;
    private ControlScheme cntrlSchm;
   

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
    private float parryTime = 1.0f;


    //Animation
    private Animator anim;

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
    //Wall Jump
    private GameObject leftWallCheck;
    private GameObject rightWallCheck;
    private bool onLeftWall;
    private bool onRightWall;
    public float wallJumpSpeed;
    public GameObject reflectHitbox;

    public enum PlayerID
    {
        p1=1, p2=2, p3=3, p4=4
    }
    public enum State
    {
        dead, alive
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
        cntrlSchm = GetComponent<ControlScheme>();
        cntrlSchm.SetControlScheme(controller);
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunController>();
        
        foreach(Transform child in transform)
        {
            if(child.name == "GroundCheck")
            {
                groundCheck = child.gameObject;
            }
            if(child.name == "WallCheckRight")
            {
                rightWallCheck = child.gameObject;
            }
            if(child.name == "WallCheckLeft")
            {
                leftWallCheck = child.gameObject;
            }

        }
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            state = State.dead;
            print(gameObject.name + " got hit by " + collision.gameObject.GetComponent<ProjectileController>().idString());
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
        //Grounded Checks
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .4f, ground);
        //Wall Jump Checks
        onLeftWall = Physics2D.OverlapCircle(leftWallCheck.transform.position, .3f, ground);
        onRightWall = Physics2D.OverlapCircle(rightWallCheck.transform.position, .3f, ground);
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
        if(horizontalInput != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
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
        //Wall Jump
        else if(Input.GetAxis(cntrlSchm.JumpAxis) > 0 && jumpKeyUp && (onLeftWall || onRightWall) && jumpKeyUp)
        {
            if (onLeftWall)
            {
                rb.velocity = new Vector2(wallJumpSpeed, wallJumpSpeed);
            }
            if (onRightWall)
            {
                rb.velocity = new Vector2(-wallJumpSpeed, wallJumpSpeed);
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
                print("Reflect Activated");
                currentlyReflecting = true;
                canReflect = false;
                reflectTimer = reflectCooldown;
                Instantiate(reflectHitbox, transform);
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

    private IEnumerator waitReflect(float reflectTime)
    {
        yield return new WaitForSeconds(reflectTime);
        currentlyReflecting = false;
    }

    public ControlScheme getControlScheme()
    {
        return GetComponent<ControlScheme>();
    }
    public bool isDead()
    {
        return state == State.dead;
    }
    public PlayerID GetID()
    {
        return playerID;
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerSpeed = 10.0f;
    public Projectile projectile;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerMovement();
        checkShootProjectile(1.0f);
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
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        }
    }

    public void checkShootProjectile(float projSpeed)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 0;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            float xDifference = mousePosition.x - transform.position.x;
            float yDifference = mousePosition.y - transform.position.y;
            float hypotenuse = MathFxns.sqRoot(MathFxns.squareNumber(yDifference) + MathFxns.squareNumber(xDifference));
            float xToHypotRatio = xDifference / hypotenuse;
            float yToHypotRatio = yDifference / hypotenuse;
            float xSpeed = projSpeed * xToHypotRatio;
            float ySpeed = projSpeed * yToHypotRatio;
            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
            p.createProjectileSpeedAndAngle(xSpeed, ySpeed);
        }
    }

}





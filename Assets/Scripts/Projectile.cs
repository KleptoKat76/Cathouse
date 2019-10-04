using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private SpriteRenderer sr;
    
    public void createProjectileSpeedAndAngle(float xSpeed, float ySpeed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xSpeed, ySpeed, 0);
        float angle = 0;
        if (ySpeed != 0)
        {
            angle = Mathf.Atan(xSpeed / ySpeed) * 180 / Mathf.PI;
            if (xSpeed < 0 && ySpeed < 0)
            {
                angle += 180;
            }
            else if (xSpeed < 0 && ySpeed > 0)
            {
                angle += 270;
            }
            else if (xSpeed > 0 && ySpeed < 0)
            {
                angle += 90;
            }
        }
        else if (xSpeed < 0)
        {
            angle = 270f;
        }
        else if (xSpeed > 0)
        {
            angle = 90f;
        }
        transform.Rotate(new Vector3(0, 0, 180 - angle));
    }

    


}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootCooldown;
    private float timer;
    private Transform nozzle;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        nozzle = transform.GetChild(0);
        timer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x,
                                                        Camera.main.transform.position.y, 0));
        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
        }
        var mousePos = ray.GetPoint(enter);
        var direction = mousePos - transform.position;
        transform.up = direction;
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
    }
    public void Shoot()
    {
        if (timer <= 0)
        {
            Instantiate(projectile, nozzle.position, nozzle.rotation);
            timer = shootCooldown;
        }
    }
}

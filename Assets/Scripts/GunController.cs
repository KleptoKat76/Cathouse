using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootCooldown;
    private float timer;
    private Transform nozzle;
    public GameObject projectile;

    private ControlScheme controlscheme;
    // Start is called before the first frame update
    void Start()
    {
        controlscheme = transform.parent.GetComponent<PlayerController>().getControlScheme();
        nozzle = transform.GetChild(0);
        timer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlscheme.GunAimXAxis.Contains("mouse"))
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
        else
        {
            if (!(Input.GetAxis(controlscheme.GunAimXAxis) == 0 && Input.GetAxis(controlscheme.GunAimYAxis) == 0)){
                transform.up = new Vector2(Input.GetAxis(controlscheme.GunAimXAxis), Input.GetAxis(controlscheme.GunAimYAxis));
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzle;

    public bool shoot;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Space"))
        {
            shoot = true;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, muzzle.position, muzzle.rotation);
    }
}

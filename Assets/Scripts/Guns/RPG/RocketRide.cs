using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRide : MonoBehaviour
{
    // for rocket damage, ammo
    rpgSystem system;

    [SerializeField] Rigidbody playerRb;
    [SerializeField] float rocketSpeed;
    public bool rocketRiding = false;
    [SerializeField] PlayerMovement playerMovement;

    void Start()
    {
        system = GetComponent<rpgSystem>();
    }

    void Update()
    {
        // shoot
        if (Input.GetKeyDown(KeyCode.Mouse1) && system.readyToShoot && !system.reloading && system.bulletsLeft > 0)
        {
            rocketRiding = true;
            ModifiedShoot();
        }

        if (rocketRiding)
        {
            playerMovement.enabled = false;
            playerRb.velocity = -transform.right * rocketSpeed;
        } else
        {
            playerMovement.enabled = true;
        }
    }

    void ModifiedShoot()
    {
        system.readyToShoot = false;

        system.bulletsLeft--;
        system.bulletsShot--;

        system.enabled = false;
    }
}

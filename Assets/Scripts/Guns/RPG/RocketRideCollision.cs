using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRideCollision : MonoBehaviour
{
    [SerializeField] RocketRide rocketRide;
    [SerializeField] rpgSystem system;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        {
            rocketRide.rocketRiding = false;

            system.enabled = true;

            system.readyToShoot = true;
        }
    }
}

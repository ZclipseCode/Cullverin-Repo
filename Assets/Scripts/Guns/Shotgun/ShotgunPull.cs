using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPull : MonoBehaviour
{
    GunSystem gunSystem;
    [SerializeField] float pullRange;
    Transform player;
    Transform enemy;

    void Start()
    {
        gunSystem = GetComponent<GunSystem>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // calculate direction with spread
            Vector3 direction = gunSystem.fpsCam.transform.forward + new Vector3(gunSystem.getX, gunSystem.getY, 0);

            // raycast
            if (Physics.Raycast(gunSystem.fpsCam.transform.position, direction, out gunSystem.rayHit, pullRange, gunSystem.whatIsEnemy))
            {
                if (gunSystem.rayHit.collider.CompareTag("Enemy"))
                {
                    enemy = gunSystem.rayHit.transform;

                    if (player.transform.position != enemy.transform.position)
                    {
                        // fix this ******************************
                        player.transform.position = new Vector3(Time.deltaTime * 0.125f, Time.deltaTime * 0.125f, Time.deltaTime * 0.125f);
                    }
                }
            }
        }
    }
}

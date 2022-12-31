using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPull : MonoBehaviour
{
    GunSystem gunSystem;
    [SerializeField] float pullRange;
    Transform player;
    Transform enemy;
    Vector3 deltaPos;
    bool enemySelected = false;
    [SerializeField] float pullSpeed;

    PullContact pullContact;

    void Start()
    {
        gunSystem = GetComponent<GunSystem>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pullContact = player.GetComponent<PullContact>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !pullContact.contacted)
        {
            // calculate direction with spread
            Vector3 direction = gunSystem.fpsCam.transform.forward + new Vector3(gunSystem.getX, gunSystem.getY, 0);

            // raycast
            if (Physics.Raycast(gunSystem.fpsCam.transform.position, direction, out gunSystem.rayHit, pullRange, gunSystem.whatIsEnemy))
            {
                if (gunSystem.rayHit.collider.CompareTag("Enemy"))
                {
                    enemy = gunSystem.rayHit.transform;

                    // magnet
                    if (!enemySelected)
                    {
                        deltaPos = player.position - enemy.position;
                        enemySelected = true;
                    }

                    if (player.transform.position != deltaPos)
                    {
                        player.transform.position -= deltaPos * Time.deltaTime * pullSpeed;
                    }
                }
            }
        }
        else
        {
            enemySelected = false;
        }
    }
}
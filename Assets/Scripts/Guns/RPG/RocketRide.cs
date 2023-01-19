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

    // audio
    [Header("Audio")]
    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip explosionAudio;
    AudioSource audioSource;
    bool playExplosion = false;

    // explosion
    [Header("Explosion")]
    [SerializeField] GameObject explosion;

    void Start()
    {
        system = GetComponent<rpgSystem>();

        // audio
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // shoot
        if (Input.GetKeyDown(KeyCode.Mouse1) && system.readyToShoot && !system.reloading && system.bulletsLeft > 0)
        {
            rocketRiding = true;
            ModifiedShoot();

            // audio
            playExplosion = true;
        }

        if (rocketRiding)
        {
            playerMovement.enabled = false;
            playerRb.velocity = -transform.right * rocketSpeed;
        } else
        {
            playerMovement.enabled = true;

            // audio
            if (playExplosion)
            {
                audioSource.PlayOneShot(explosionAudio);

                // explosion
                Instantiate(explosion, playerRb.position, Quaternion.identity);

                playExplosion = false;
            }
        }
    }

    void ModifiedShoot()
    {
        system.readyToShoot = false;

        system.bulletsLeft--;
        system.bulletsShot--;

        system.enabled = false;

        // audio
        audioSource.PlayOneShot(shootAudio);
    }
}

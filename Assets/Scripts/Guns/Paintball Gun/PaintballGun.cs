using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaintballGun : MonoBehaviour
{
    // gun stats
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    private int bulletsLeft, bulletsShot;

    // bools
    private bool shooting, readyToShoot, reloading;

    // reference
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RaycastHit rayHit;
    [SerializeField] private LayerMask whatIsEnemy;

    // graphics
    [SerializeField] private GameObject muzzleFlash, bulletHoleGraphic;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Sound")]
    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip reloadAudio;
    AudioSource audioSource;

    // paint
    [Header("Paint")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject paintGraphic;
    [SerializeField] GameObject slipperyArea;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        PlayerInput();

        // set text
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void PlayerInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);

                Instantiate(paintGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }  
        }

        // raycast paint
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsGround))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Ground"))
            {
                // graphics
                Instantiate(paintGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                Instantiate(slipperyArea, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }
        }

        // graphics
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting); // invoke calls a function with a bit of delay

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        audioSource.PlayOneShot(shootAudio);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

        audioSource.PlayOneShot(reloadAudio);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

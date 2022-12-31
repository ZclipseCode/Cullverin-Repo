using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rpgSystem : MonoBehaviour
{
    // gun stats
    [SerializeField] public float timeBetweenShooting, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    private int bulletsLeft, bulletsShot;

    // bools
    private bool shooting, readyToShoot, reloading;

    // reference
    [SerializeField] public Camera fpsCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] public RaycastHit rayHit;

    // graphics
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Sound")]
    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip reloadAudio;
    AudioSource audioSource;

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

        // graphics
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(fpsCam.transform.rotation.eulerAngles));

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

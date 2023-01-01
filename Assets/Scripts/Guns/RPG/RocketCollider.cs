using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollider : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] float rocketSpeed;
    Rigidbody rb;
    [SerializeField] GameObject colGo;
    [SerializeField] float rocketTime;
    float currentTime;

    [Header("Audio")]
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip explosionAudio;
    AudioSource audioSource;
    [SerializeField] float audioLength;

    bool exploded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        rb.velocity = transform.forward * rocketSpeed;

        currentTime += Time.deltaTime;
        if (currentTime >= rocketTime && !exploded)
        {
            StartCoroutine(Explode());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Enemy")
        {
            StartCoroutine(Explode());
        }
    }

    //void Explode()
    //{
    //    Instantiate(explosion, colGo.transform.position, Quaternion.identity);
    //    audioSource.PlayOneShot(explosionAudio);
    //    Destroy(gameObject);
    //}

    IEnumerator Explode()
    {
        exploded = true;

        Instantiate(explosion, colGo.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(explosionAudio);

        yield return new WaitForSeconds(audioLength);

        Destroy(gameObject);
    }
}

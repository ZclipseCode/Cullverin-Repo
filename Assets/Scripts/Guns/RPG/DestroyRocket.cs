using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRocket : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float deleteTime;
    float currentTime = 0;
    //Transform rocketTrail;

    void Start()
    {
        
    }

    void Update()
    {
        //rocketTrail = GameObject.FindGameObjectWithTag("Rocket Trail").GetComponent<Transform>();

        currentTime += Time.deltaTime;

        if (currentTime >= deleteTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

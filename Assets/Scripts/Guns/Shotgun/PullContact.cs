// attaches to player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullContact : MonoBehaviour
{
    public bool contacted = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            contacted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            contacted = false;
        }
    }
}

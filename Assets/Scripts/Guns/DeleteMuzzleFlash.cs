using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMuzzleFlash : MonoBehaviour
{
    float deleteTime = 1;
    float currentTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= deleteTime)
        {
            Destroy(gameObject);
        }
    }
}

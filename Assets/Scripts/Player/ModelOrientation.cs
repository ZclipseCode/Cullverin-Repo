using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelOrientation : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject model;

    void Start()
    {
        
    }

    void Update()
    {
        model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
    }
}

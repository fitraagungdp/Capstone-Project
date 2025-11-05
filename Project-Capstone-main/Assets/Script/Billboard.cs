using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    public bool billboardEnabled = true;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (billboardEnabled && mainCamera != null)
        {
            // Make the object face the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}

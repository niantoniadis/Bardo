using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}

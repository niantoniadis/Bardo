using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Camera cam;
    public GameObject target;

    void LateUpdate()
    {
        cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, cam.transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(target.position.y);
        gameObject.transform.position = new Vector3(transform.position.x, Mathf.Clamp(target.position.y,-29.7f,29.7f), transform.position.z);
    }
}

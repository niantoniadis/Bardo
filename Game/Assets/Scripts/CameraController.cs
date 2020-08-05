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
        gameObject.transform.position = new Vector3(Mathf.Clamp(target.position.x,-6.5f, 6.5f), Mathf.Clamp(target.position.y,-29.7f,29.7f), transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this.gameObject, 10f);
    }

    void Start()
    {
        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;
        Debug.Log(mousePos);
    }

    void Update()
    {
        this.gameObject.transform.position += new Vector3(mousePos.x * 10f * Time.deltaTime, mousePos.y * 10f * Time.deltaTime, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}

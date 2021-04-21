using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this.gameObject, 10f);
    }

    void Start()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(mousePos.x, mousePos.y) * 120f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}

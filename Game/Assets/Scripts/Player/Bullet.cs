using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this.gameObject, 2f);
    }

    void Start()
    {
        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;
        Debug.Log(mousePos);
    }

    void Update()
    {
        this.gameObject.transform.position += Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0).normalized, 10.0f * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NewEnemy>() != null)
            Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    PlayerMovement player;
    float timeAlive = 0;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;
        this.transform.up = new Vector3(mousePos.x, mousePos.y);
        Debug.Log(mousePos);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (!player.isPaused)
            this.gameObject.transform.position += Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0).normalized, 10.0f * Time.deltaTime);

        if (timeAlive >= 2.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NewEnemy>() != null)
            Destroy(this.gameObject);
    }
}

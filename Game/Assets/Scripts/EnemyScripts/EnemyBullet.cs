using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player player;
    float timeAlive = 0;
    private Vector2 moveDirection;
    private Rigidbody2D rb2d;
   // private float moveSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = this.GetComponent<Rigidbody2D>();
        moveDirection = (player.transform.position - this.transform.position).normalized;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);
        transform.up = moveDirection;
    }

    void Update()
    {
        if (!player.isPaused)
            this.gameObject.transform.position += Vector3.ClampMagnitude(new Vector3(moveDirection.x, moveDirection.y, 0).normalized, 7.5f * Time.deltaTime);

        timeAlive += Time.deltaTime;
        if (timeAlive >= 4.0f)
        {
            Destroy(this.gameObject);
        }
    }
}

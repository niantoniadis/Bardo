using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snarko : Character
{
    public Character target;

    bool onScreen;
    // Start is called before the first frame update
    protected override void Start()
    {
        position = transform.position;
        maxHealth = 25;
        health = maxHealth;
        speed = 18f;
        maxSpeed = 7f;
        direction = new Vector2(1, 0);
        rotation = Mathf.Atan2(direction.y, direction.x);
        mass = 0.5f;

        friction = 0.95f;
    }

    protected override void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            CalcSteeringForces();
            Movement();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame

    public override void CalcSteeringForces()
    {
        Vector2 forceDirection = Vector2.zero;
        forceDirection += Pursue(target);
        forceDirection.Normalize();

        ApplyForce(forceDirection * speed);
        ApplyFriction(friction);
    }

    public override void Movement()
    {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;
        RotateCharacter();

        acceleration = Vector2.zero;
    }

    public override void RotateCharacter()
    {
        direction = target.Position - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        rotation = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            health -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }
}

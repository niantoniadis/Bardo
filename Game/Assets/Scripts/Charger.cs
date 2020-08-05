using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Character
{
    public Transform target;


    Vector2 forceDirection = Vector2.zero;
    float chargeSpeed;
    float chargeCooldown;
    float chargeTimer;
    bool posRetrieved;
    float desiredRotation;
    // Start is called before the first frame update
    protected override void Start()
    {
        posRetrieved = false;

        chargeCooldown = 2.3f;
        chargeTimer = 1f;

        position = transform.position;
        maxHealth = 60;
        health = maxHealth;
        speed = 50f;
        chargeSpeed = 400f;
        maxSpeed = 7f;
        direction = new Vector2(1, 0);
        rotation = Mathf.Atan2(direction.y, direction.x);
        mass = 3f;

        friction = 0.90f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        CalcSteeringForces();
        Movement();
        RotateCharacter();
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }

    public override void CalcSteeringForces()
    {
        chargeCooldown -= Time.deltaTime;
        if (chargeCooldown <= 0)
        {
            if (!posRetrieved) 
            {
                forceDirection = (target.position - transform.position).normalized;
                posRetrieved = true;
            }
            ApplyForce(forceDirection * chargeSpeed);
            chargeTimer -= Time.deltaTime;
            if(chargeTimer <= 0)
            {
                chargeCooldown = 2.3f;//Random.Range(1.25f, 3.5f);
                chargeTimer = 1f; //chargeCooldown / 2;
            }
        }
        else
        {
            forceDirection = (target.position - transform.position).normalized;
            ApplyForce(forceDirection * speed);
            posRetrieved = false;
        }

        direction = forceDirection.normalized;
        //desiredRotation = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        rotation = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);

        ApplyFriction(friction);
    }

    public override void RotateCharacter()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            health -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }
}

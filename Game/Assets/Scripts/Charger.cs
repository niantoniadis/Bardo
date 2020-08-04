using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Character
{
    public Transform target;

    float chargeSpeed;
    float chargeCooldown;
    float chargeTimer;
    bool posRetirieved;
    // Start is called before the first frame update
    protected override void Start()
    {
        posRetirieved = false;

        chargeCooldown = 2.3f;

        position = transform.position;
        maxHealth = 15;
        health = maxHealth;
        speed = 5f;
        chargeSpeed = 150f;
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
    }

    public override void CalcSteeringForces()
    {
        Vector2 forceDirection = Vector2.zero;

        chargeCooldown -= Time.deltaTime;
        if (chargeCooldown <= 0)
        {
            if (!posRetirieved) 
            {
                forceDirection = target.position;
                posRetirieved = true;
            }
            ApplyForce(forceDirection * chargeSpeed);
            chargeTimer -= Time.deltaTime;
            if(chargeTimer <= 0)
            {
                chargeCooldown = Random.Range(1.25f, 3.5f);
                chargeTimer = chargeCooldown / 4;
                posRetirieved = false;
            }
        }
        else
        {
            forceDirection = target.position;
            ApplyForce(forceDirection * speed);
        }

        //ApplyFriction(friction);
    }
}

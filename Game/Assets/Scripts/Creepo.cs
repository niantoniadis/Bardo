﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creepo : Character
{
    public Transform target;

    // Start is called before the first frame update
    protected override void Start()
    {
        position = transform.position;
        maxHealth = 15;
        health = maxHealth;
        speed = 15f;
        maxSpeed = 7f;
        direction = new Vector3(1, 0, 0);
        rotation = Mathf.Atan2(direction.y, direction.x);
        mass = 0.5f;

        friction = 0.95f;
    }

    protected override void Update()
    {
        CalcSteeringForces();
        Movement();
    }

    // Update is called once per frame

    public override void CalcSteeringForces()
    {
        Vector2 forceDirection = Vector2.zero;
        forceDirection += Seek(new Vector2(target.position.x, target.position.y));
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

        acceleration = Vector3.zero;
    }

    public override void RotateCharacter()
    {
        direction = target.position - transform.position;
        direction.Normalize();
        rotation = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Character
{
    Animator animator;

    GameObject guitar;
    // Start is called before the first frame update
    protected override void Start()
    {
        maxHealth = 80;
        health = maxHealth;
        speed = 100f;
        direction = new Vector2(1, 0);
        rotation = Mathf.Atan2(direction.y, direction.x);
        mass = 1f;
        maxSpeed = 5f;

        friction = 0.85f;

        animator = GetComponent<Animator>();

        guitar = GameObject.FindGameObjectWithTag("Guitar");
    }

    // Update is called once per frame
    protected override void Update()
    {
        CalcSteeringForces();
        Movement();

    }

    public override void CalcSteeringForces()
    {
        Vector3 forceDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            forceDirection = new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            forceDirection += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            forceDirection += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            forceDirection += new Vector3(0, -1, 0);
        }
        forceDirection.Normalize();

        ApplyForce(forceDirection * speed);
        ApplyFriction(friction);

        if (velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    public override void Movement()
    {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;

        acceleration = Vector3.zero;
    }

    public void Throw()
    {

    }
}

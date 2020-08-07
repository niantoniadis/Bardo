using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Character
{
    Animator animator;

    Guitar guitar;
    public GameObject guitarBullet;

    Transform gamePosition;
    Vector2 gamePos;

    float guitarShotCoolDown;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        guitar = GameObject.FindGameObjectWithTag("Guitar").GetComponent<Guitar>();

        maxHealth = 80;
        health = maxHealth;
        speed = 100f;
        direction = new Vector2(1, 0);
        rotation = Mathf.Atan2(direction.y, direction.x);
        mass = 1f;
        maxSpeed = 5f;

        friction = 0.85f;

        guitarShotCoolDown = 0.5f;

        gamePosition = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        position = transform.position;
        gamePos = gamePosition.position + transform.position;
        CalcSteeringForces();
        Movement();

        if (velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        guitarShotCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && guitarShotCoolDown <= 0)
        {
            Instantiate(guitarBullet, transform.position, Quaternion.identity);
            guitarShotCoolDown = 0.5f;
        }
        if (Input.GetMouseButton(1) && !guitar.Thrown)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwPos = (mousePos - transform.position).normalized * 8f;
            guitar.Throw(throwPos);
        }
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
    }

    public override void Movement()
    {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;

        acceleration = Vector3.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            health -= 5; //collision.gameObject.GetComponent<Enemy>().Damage;
        }
        if (collision.gameObject.layer == 13)
        {
            ApplyForce((transform.position - collision.collider.bounds.center).normalized * (velocity.sqrMagnitude / 2));
        }
    }
}

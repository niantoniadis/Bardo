using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Character
{
    public Transform target;

    int damage;
    bool onScreen = false;

    public int Damage
    {
        get
        {
            return damage;
        }
    }
    // Start is called before the first frame update
    new void Start()
    {
        position = transform.position;

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    // Update is called once per frame
    new void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void RotateCharacter()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            ApplyForce((transform.position - collision.collider.bounds.center).normalized * (velocity.sqrMagnitude/2));
        }
    }
}

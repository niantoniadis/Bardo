using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    public GameObject sprite;
    public Bard bard;

    CapsuleCollider2D collider1;
    CircleCollider2D collider2;

    Vector2 bardOffset;
    Vector3 velocity;
    Vector3 desiredVelocity;
    Vector3 target;
    bool targetReached = false;
    bool thrown = false;

    public bool Thrown
    {
        get
        {
            return thrown;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bardOffset = transform.localPosition;

        collider1 = GetComponent<CapsuleCollider2D>();
        collider2 = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (thrown)
        {
            if (!targetReached)
            {
                desiredVelocity = target - transform.position;
                if (desiredVelocity.sqrMagnitude < 1f)
                {
                    targetReached = true;
                }
            }
            else
            {
                desiredVelocity = (bard.Position + bardOffset) - new Vector2(transform.position.x, transform.position.y);
            }
            velocity += (desiredVelocity.normalized * 2f - velocity) * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;

            if(targetReached && desiredVelocity.sqrMagnitude < 1f)
            {
                transform.position = bard.Position + bardOffset;
                thrown = false;
            }
        }
        else if(collider1.enabled || collider2.enabled)
        {
            collider1.enabled = false;
            collider2.enabled = false;
        }
    }

    public void Throw(Vector2 targetPos)
    {
        thrown = true;
        target = targetPos;
        float rotation = Mathf.Rad2Deg * Mathf.Atan2(targetPos.y,targetPos.x) + Mathf.PI;
        velocity = new Vector3(0, 1, 0);//2 * Mathf.Acos(rotation),2 * Mathf.Asin(rotation),0);
    }
}

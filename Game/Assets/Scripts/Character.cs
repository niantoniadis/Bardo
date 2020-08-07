using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Vector2 direction;
    protected Vector2 position;
    protected Vector2 velocity;
    protected Vector2 desiredVelocity;
    protected Vector2 acceleration;
    protected float rotation;
    protected float mass = 1;
    protected float friction;
    protected float maxSpeed;
    protected float radius;
    protected float wanderRotation;
    protected float wanderTimer;
    protected float safeDistance;
    protected Vector2 wanderPos;
    protected float boundsTimer;

    protected int health;
    protected int maxHealth;
    protected float speed;
    protected float baseDamage;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        direction = new Vector2(1, 0);
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        position = transform.position;
        radius = 0.5f;
        wanderRotation = 180;
        friction = 0.8f;
        safeDistance = 8f;

        mass = 1;
        maxSpeed = 5f;

        wanderTimer = 0f;
        boundsTimer = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalcSteeringForces();
        Movement();
        //if (boundsTimer >= 0)
        //{
        //    boundsTimer -= Time.deltaTime;
        //}
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
    }

    public Vector2 Position
    {
        get
        {
            return position;
        }
    }
    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public float Mass
    {
        get
        {
            return mass;
        }
        set
        {
            mass = value;
        }
    }

    public virtual void Movement()
    {
        velocity += acceleration * Time.deltaTime;
        Vector2.ClampMagnitude(velocity, maxSpeed);
        position += velocity * Time.deltaTime;

        transform.position = position;
        RotateCharacter();

        acceleration = Vector2.zero;
    }

    public void ApplyForce(Vector2 force)
    {
        acceleration += (force / mass);
    }

    public Vector2 Flee(Vector2 pos)
    {
        desiredVelocity = new Vector2(position.x - pos.x, position.y - pos.y);
        desiredVelocity = Vector2.ClampMagnitude(desiredVelocity, maxSpeed);
        return (desiredVelocity - velocity);
    }

    public Vector2 Evade(Character chaser)
    {
        Vector2 pos = chaser.position + chaser.velocity.normalized * 2;
        Vector2 playerDist = position - chaser.position;
        if ((chaser.velocity.normalized * 2).sqrMagnitude >= playerDist.sqrMagnitude)
        {
            return Flee(chaser.Position);
        }
        return Flee(pos);
    }

    public Vector2 Seek(Vector2 pos)
    {
        desiredVelocity = new Vector2(pos.x - position.x, pos.y - position.y);
        desiredVelocity.Normalize();
        Vector2 force = desiredVelocity * 2f - velocity.normalized;
        force.Normalize();
        return force;
    }

    public Vector2 Pursue(Character target)
    {
        Vector2 pos = target.position + target.velocity.normalized * 5f;
        Vector2 playerDist = position - target.position;
        return Seek(pos);
    }

    public Vector2 Wander()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
        {
            wanderTimer = 0.05f;
            Vector2 pos = Position + velocity.normalized * 5;
            wanderRotation += Random.Range(-10f, 10f);
            wanderRotation %= 360f;
            pos = new Vector2(pos.x + Mathf.Cos(wanderRotation) * 1.5f, pos.y + Mathf.Sin(wanderRotation) * 1.5f);
            wanderPos = pos;
            return Seek(wanderPos);
        }
        else
        {
            return Seek(wanderPos);
        }
    }

    public void ApplyFriction(float coef)
    {
        velocity *= coef;
    }

    public virtual void RotateCharacter()
    {
        direction = velocity.normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.Cross(Vector3.Cross(direction, new Vector3(0, 1, 0)), direction));
    }

    /*
    public Vector2 ObstacleAvoidance(List<Obstacle> obstacles)
    {
        float dist;
        Vector2 ultimateForce = Vector2.zero;

        foreach (Obstacle ob in obstacles)
        {
            dist = (ob.transform.position - position).magnitude;

            Vector2 vecToObstacle = ob.transform.position - position;
            float distRight = Vector2.Dot(vecToObstacle, transform.right);
            float distForward = Vector2.Dot(vecToObstacle, transform.forward);
            float radiiSum = ob.Radius + radius;

            if (position.y <= ob.Height && radiiSum >= Mathf.Abs(distRight) && distForward > 0 && vecToObstacle.sqrMagnitude < safeDistance * safeDistance)
            {
                Vector2 desiredVelocity;

                if (distRight < 0)
                {
                    desiredVelocity = transform.right * maxSpeed;
                }
                else
                {
                    desiredVelocity = -transform.right * maxSpeed;
                }

                Vector2 steeringForce = desiredVelocity - velocity;
                ultimateForce += steeringForce / mass;
            }
        }
        return ultimateForce;
    }
    */
    public Vector2 AddBoundaryForce()
    {
        if (position.x < -38.5f || position.x > 35.5f || position.y < -40f || position.y > 36f)
        {
            boundsTimer = 0.2f;
            return Seek(new Vector2(0, 15));
        }
        if (boundsTimer > 0)
        {
            return Seek(new Vector2(0, 15));
        }

        return Vector2.zero;
    }

    /*
    public Vector2 Separate(List<Boid> neighbors)
    {
        Vector2 ultimateForce = Vector2.zero;

        foreach (Vehicle neighbor in neighbors)
        {
            Vector2 desiredVelocity = position - neighbor.position;
            float mag = desiredVelocity.magnitude;
            if (mag > 0 && mag <= 10f)
            {
                ultimateForce += desiredVelocity.normalized / mag;
            }
        }

        Vector2.ClampMagnitude(ultimateForce, maxSpeed);

        return ultimateForce;
    }
    */

    public Vector2 Cohere(Vector2 centroid)
    {
        return Seek(centroid);
    }

    public Vector2 Align(Vector2 flockDirection)
    {
        Vector2 desiredVelocity = flockDirection.normalized * maxSpeed;

        return desiredVelocity - velocity;
    }

    public abstract void CalcSteeringForces();
}
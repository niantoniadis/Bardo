using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Angel,
    Demon
}

public enum BuffType
{
    Damage,
    Health,
    Speed,
    Defense,
    Angelic,
    Demonic
}

public abstract class NewEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public EnemyType eType;
    public BuffType bType;

    public int damage;
    public int health;
    public int defense;
    public int speed;

    public int floor;
    public int roomIndex;
    public bool possessed;
    public bool isActive;

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        possessed = false;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!possessed)
        {
            // update code here
        }
        else
        {
            // other update code here
            Movement();
        }
    }

    /// <summary>
    /// Sets the enemies stats based on the
    /// floor number and the buff type
    /// </summary>
    public void SetStats()
    {
        floor = Mathf.Abs(floor);
        int[] bases = { 3, 1, 1, 20};
        int[] typeMods = { 0, 0, 0, 0 };

        // set the base values not on floor 1
        if (floor == 1)
        {
            bases[0] += 3;
            bases[1] += 2;
            bases[2] += 1;
            bases[3] += 12;
        }
        else if (floor >= 2)
        {
            bases[0] += 6;
            bases[1] += 4;
            bases[2] += 2;
            bases[3] += 24;
        }

        // set the modifiers by class type
        switch (bType)
        {
            case BuffType.Damage:
                typeMods[0] += 3;
                typeMods[1] += -1;
                typeMods[2] += 0;
                typeMods[3] += 0;
                break;

            case BuffType.Defense:
                typeMods[0] += -1;
                typeMods[1] += 3;
                typeMods[2] += 0;
                typeMods[3] += 0;
                break;

            case BuffType.Speed:
                typeMods[0] += 0;
                typeMods[1] += 0;
                typeMods[2] += 3;
                typeMods[3] += -4;
                break;

            case BuffType.Health:
                typeMods[0] += 0;
                typeMods[1] += 0;
                typeMods[2] += -1;
                typeMods[3] += 12;
                break;
        }

        // set modifiers based on enemy type
        switch (eType)
        {
            case EnemyType.Normal:
                typeMods[0] += 1;
                typeMods[1] += 1;
                typeMods[2] += 1;
                typeMods[3] += 4;
                break;

            case EnemyType.Angel:
                typeMods[0] += 0;
                typeMods[1] += 0;
                typeMods[2] += 2;
                typeMods[3] += 8;
                break;

            case EnemyType.Demon:
                typeMods[0] += 2;
                typeMods[1] += 2;
                typeMods[2] += 0;
                typeMods[3] += 0;
                break;
        }

        // set stats
        damage = bases[0] + typeMods[0];
        defense = bases[1] + typeMods[1];
        speed = bases[2] + typeMods[2];
        health = bases[3] + typeMods[3];
    }

    /// <summary>
    /// Makes the enemy move towards the player
    /// </summary>
    public void Movement()
    {
        // seeks player
        Vector2 desired = (Vector2)playerMovement.transform.position - position;

        // gets the desired velocity to seek player
        desired.Normalize();
        desired = desired * speed;
        desired -= velocity;
        desired = Vector2.ClampMagnitude(desired, speed);
        
        // adds to acceleration
        acceleration += desired;

        // sets vectors
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        acceleration = Vector3.zero;

        // sets the direction
        direction = velocity.normalized;
        transform.forward = direction;

        // formats the position
        transform.position = position;
    }
}

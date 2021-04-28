using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public float speed;
    Vector2 offset;
    Collider movementBounds;
    public bool isPossessing;

    public Collider MovementBounds
    {
        set
        {
            movementBounds = value;
        }
    }

    enum State
    {
        Move,
        Stand,
        Guard,
        Dash
    };

    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Stand;
    }

    // Update is called once per frame
    void Update()
    {
        //WasdMovement();
    }

    private void FixedUpdate()
    {
        if (!isPossessing)
        { WasdMovement(); }
    }

    void WasdMovement()
    {
        offset = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            currentState = State.Move;
            offset += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentState = State.Move;
            offset += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentState = State.Move;
            offset += new Vector2(0, -1);

        }
        if (Input.GetKey(KeyCode.D))
        {
            currentState = State.Move;
            offset += new Vector2(1, 0);
        }
        if(offset == Vector2.zero)
        {
            currentState = State.Stand;
        }
        else
        {
            offset.Normalize();
            transform.position += Time.fixedDeltaTime * speed * new Vector3(offset.x, offset.y, 0);
        }
    }
}

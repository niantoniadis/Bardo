using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.03f;

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
        WasdMovement();
    }

    void WasdMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentState = State.Move;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed, player.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentState = State.Move;
            player.transform.position = new Vector3(player.transform.position.x - speed, player.transform.position.y, player.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentState = State.Move;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - speed, player.transform.position.z);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentState = State.Move;
            player.transform.position = new Vector3(player.transform.position.x + speed, player.transform.position.y, player.transform.position.z);

        }
        else
        {
            currentState = State.Stand;
        }
    }
}

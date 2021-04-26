using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public GameObject player;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = (GameObject)Resources.Load("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(bullet, player.transform.position, Quaternion.identity);
        }
    }
}

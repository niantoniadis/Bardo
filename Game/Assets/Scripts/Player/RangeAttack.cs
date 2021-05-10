using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public Player player;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = (GameObject)Resources.Load("Bullet");
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.room != null)
        {
            player.room.bullets.Add(Instantiate(bullet, player.transform.position, Quaternion.identity));
        }
    }
}

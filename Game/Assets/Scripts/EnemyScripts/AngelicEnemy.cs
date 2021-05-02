using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelicEnemy : NewEnemy
{
    private GameObject bullet;
    float timeForNextAtk = 1f;

    // Start is called before the first frame update
    void Start()
    {
        eType = EnemyType.Angel;
        bType = BuffType.Health;
        bullet = (GameObject)Resources.Load("EnemyBullet");
    }

    void Update()
    {
        if (!isPaused)
        {
            if (timeForNextAtk > 0f)
            {
                timeForNextAtk -= Time.deltaTime;
            }
            else if (timeForNextAtk <= 0f)
            {
                Attack();
                timeForNextAtk = 1f;
            }
        }
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        // remove later
        //SetStats();
    }

    public override void Attack()
    {
        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }
}

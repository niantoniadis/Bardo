using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicEnemy : NewEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        eType = EnemyType.Demon;
        bType = BuffType.Health;
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

    }

}

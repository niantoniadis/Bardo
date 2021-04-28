using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelicEnemy : NewEnemy
{


    // Start is called before the first frame update
    void Start()
    {
        eType = EnemyType.Angel;
        bType = BuffType.Health;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // remove later
        //SetStats();
    }

    public override void Attack()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicEnemy : NewEnemy
{
    public Animator animator;

    //EnemyAttack
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange = 1.2f;
    public DemonicEnemy thisEnemy;
    float timeForNextAtk = 2f;

    // Start is called before the first frame update
    void Start()
    {
        eType = EnemyType.Demon;
        bType = BuffType.Health;
    }

    void Update()
    {
        if (!isPaused)
        {
            if (timeForNextAtk > 0f)
            {
                animator.SetBool("isAttacking", true);
                timeForNextAtk -= Time.deltaTime;
            }
            else if (timeForNextAtk <= 0f)
            {
                animator.SetBool("isAttacking", false);
                Attack();
                timeForNextAtk = 2f;
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
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            Debug.Log("Hit " + hitPlayer.name);
            hitPlayer.GetComponent<Player>().TakeDamage(thisEnemy.damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

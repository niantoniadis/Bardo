using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 10;
    public int def = 10;
    public int atk = 10;
    public int damageToAngel = 10;
    public int damageToDevil = 10;

    public int maxHealth = 100;
    public int currentHealth;

    public int floorLevel;
    public Room room;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taken damage called:" + damage);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBullet>() != null)
        {
            Debug.Log(collision);
            TakeDamage(5);
            Destroy(collision.gameObject);
        }
    }
}

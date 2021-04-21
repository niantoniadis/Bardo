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
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void CloseAttack(int targetHealth, int targetDef, string type)
    {
        if (type == "Angel")
        {
            targetHealth -= (atk - targetDef) * damageToAngel / 10;
        }
        else
        {
            targetHealth -= (atk - targetDef) * damageToDevil / 10;
        }
    }
}

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
    public bool isPaused;

    public int floorLevel;
    public Room room;

    public List<Buff> buffs;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        buffs = new List<Buff>();
        isPaused = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (room == null)
        {
            GameObject r = GameObject.Find("Room(Clone)");
            if (r != null)
            {
                room = r.GetComponent<Room>();
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        speed += buff.speedMod;
        atk += buff.damageMod;
        def += buff.defenseMod;
        maxHealth += buff.healthMod;
        currentHealth += buff.healthMod;
        healthBar.SetMaxHealth(maxHealth);
        buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        speed -= buff.speedMod;
        atk -= buff.damageMod;
        def -= buff.defenseMod;
        maxHealth -= buff.healthMod;
        currentHealth -= buff.healthMod;
        healthBar.SetMaxHealth(maxHealth);
        buffs.Remove(buff);
    }

    public void TakeDamage(int damage)
    {
        if (buffs.Count != 0 && Random.Range(0, 10) < 3)
        {
            RemoveBuff(buffs[0]);
            Debug.Log("Removing the first buff");
        }

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

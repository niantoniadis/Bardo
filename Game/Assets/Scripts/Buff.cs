using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public int healthMod;
    public int defenseMod;
    public int damageMod;
    public int speedMod;

    public bool removable;
    public BuffType type;

    public Buff(int hp, int def, int dam, int spd, bool canRemove, BuffType t)
    {
        healthMod = hp;
        defenseMod = def;
        damageMod = dam;
        speedMod = spd;
        removable = canRemove;
        type = t;
    }
    public Buff(int floor, BuffType bt, EnemyType et)
    {
        removable = true;
        type = bt;

        // adds based on enemy
        switch (et)
        {
            case EnemyType.Angel:
                healthMod = 4 * floor + 4;
                damageMod = floor;
                speedMod = floor + 1;
                defenseMod = floor;
                break;

            case EnemyType.Demon:
                healthMod = 4 * floor;
                damageMod = floor + 1;
                speedMod = floor;
                defenseMod = floor + 1;
                break;

            default:
                healthMod = 4 * floor;
                damageMod = floor;
                speedMod = floor;
                defenseMod = floor;
                break;
        }

        // adds based on buff
        switch (bt)
        {
            case BuffType.Health:
                healthMod += 4;
                break;

            case BuffType.Damage:
                damageMod += 1;
                break;

            case BuffType.Defense:
                defenseMod += 1;
                break;

            case BuffType.Speed:
                speedMod += 1;
                break;
        }
    }

    public virtual void Activate()
    {
        // add all the bonuses to the player
    }

    public virtual void Deactivate()
    {
        // remove all the bonuses here
    }
}

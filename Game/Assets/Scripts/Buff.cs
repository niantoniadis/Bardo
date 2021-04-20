using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public Character character;

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

    public virtual void Activate()
    {
        // add all the bonuses to the player
    }

    public virtual void Deactivate()
    {
        // remove all the bonuses here
    }
}

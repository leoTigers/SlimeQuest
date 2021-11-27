using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum Status
{
    NONE,
    POISONED,
    PARALIZED,
    CONFUSED,
    BURNED,
    DEAD
}

public class Entity
{
    public string name;
    public int hp, hpMax;
    public int mp, mpMax;

    public int physicalAtt, physicalDef;
    public int magicalAtt, magicalDef;
    public int speed, luck;
    public int level, xp, xpToNextLevel;
    public Status entityStatus;
    // Sprite location
    public string spriteLocation;

    public Entity(string name, int hpMax, int mpMax, int physicalAtt, int physicalDef, int magicalAtt, int magicalDef, int speed, int luck, int level, string spriteLocation)
    {
        this.name = name;
        this.hpMax = hpMax;
        hp = hpMax;
        this.mpMax = mpMax;
        mp = mpMax;
        this.physicalAtt = physicalAtt;
        this.physicalDef = physicalDef;
        this.magicalAtt = magicalAtt;
        this.magicalDef = magicalDef;
        this.speed = speed;
        this.luck = luck;
        this.level = level;
        this.spriteLocation = spriteLocation;
        entityStatus = Status.NONE;
    }

    public Entity(string name, int hp, int hpMax, int mp, int mpMax, int physicalAtt, int physicalDef, int magicalAtt, int magicalDef, int speed, int luck, Status entityStatus, string spriteLocation)
    {
        this.name = name;
        this.hp = hp;
        this.hpMax = hpMax;
        this.mp = mp;
        this.mpMax = mpMax;
        this.physicalAtt = physicalAtt;
        this.physicalDef = physicalDef;
        this.magicalAtt = magicalAtt;
        this.magicalDef = magicalDef;
        this.speed = speed;
        this.luck = luck;
        this.entityStatus = entityStatus;
        this.spriteLocation = spriteLocation;
    }

    /**
     * Reduce hp by the amount
     * Return true if this result in a death, false otherwise
     * */
    public bool TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0)
        {
            hp = 0;
            entityStatus = Status.DEAD;
            return true;
        }
        return false;
    }
}


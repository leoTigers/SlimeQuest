using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public enum Status
{
    NONE,
    POISONED,
    PARALIZED,
    CONFUSED,
    BURNED,
    DEAD
}

public interface IHittable
{
    /**
     * Reduce hp by the amount
     * Return true if this result in a death, false otherwise
     * */
    public bool TakeDamage(int damage);
    public bool IsDefending { get; set; }
}

public interface ILevelable
{
    public int Xp { get; set; }
    public int Level { get; set; }
}

public class EntityStatistics
{

}

[Serializable]
public class Entity: IHittable, ILevelable
{
    public Entity()
    {

    }

    public Entity(string spriteLocation="", string name="Unknown", int hp=-1, int hpMax=100, int mp=-1, int mpMax=25, int physicalAttack=5, int physicalDefense=5,
        int magicalAttack=5, int magicalDefense=5)
    {
        Hp = hp==-1?hpMax:hp;
        HpMax = hpMax;
        Mp = mp==-1?mpMax:mp;
        MpMax = mpMax;
        PhysicalAttack = physicalAttack;
        PhysicalDefense = physicalDefense;
        MagicalAttack = magicalAttack;
        MagicalDefense = magicalDefense;
        SpriteLocation = spriteLocation;
        Name = name;
        EntityStatus = Status.NONE;
        Xp = 0;
        Level = 1;
        IsDefending = false;
    }

    // Sprite location
    public string SpriteLocation { get; set; }
    public Status EntityStatus { get; set; }
    public string Name { get ; set ; }
    public int Xp { get ; set ; }
    public int Level { get ; set ; }
    public bool IsDefending { get ; set ; }
    public int Hp { get; set; }
    public int HpMax { get; set; }
    public int Mp { get; set; }
    public int MpMax { get; set; }
    public int PhysicalAttack { get; set; }
    public int PhysicalDefense { get; set; }
    public int MagicalAttack { get; set; }
    public int MagicalDefense { get; set; }

    public bool TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            EntityStatus = Status.DEAD;
            return true;
        }
        return false;
    }

    static public void Save(Entity e, string filename="save.json")
    {

        XmlSerializer ser = new XmlSerializer(typeof(Entity));
        TextWriter writer = new StreamWriter(filename);

        ser.Serialize(writer, e);
        writer.Close();
    }
}


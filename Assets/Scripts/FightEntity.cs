using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Text.Json;

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

public class Entity: IHittable, ILevelable
{
    public Entity(string name, Dictionary<string, int> statistics, string spriteLocation="")
    {
        this.spriteLocation = spriteLocation;
        Name = name;
        EntityStatus = Status.NONE;
        Statistics = new Dictionary<string, int>();
        foreach (string key in statistics.Keys)
        {
            Statistics[key] = statistics[key];
            if (key == "HpMax")
                Statistics["Hp"] = statistics[key];
            if (key == "MpMax")
                Statistics["Mp"] = statistics[key];
        }
            
        Xp = 0;
        Level = 1;
        IsDefending = false;
    }

    // Sprite location
    public string spriteLocation { get; set; }
    public Status EntityStatus { get; set; }
    public string Name { get ; set ; }
    public Dictionary<string, int> Statistics { get; set; }
    public int Xp { get ; set ; }
    public int Level { get ; set ; }
    public bool IsDefending { get ; set ; }

    public bool TakeDamage(int damage)
    {
        int Hp = Statistics["Hp"];
        Hp -= damage;
        if (Hp < 0)
        {
            Statistics["Hp"] = 0;
            EntityStatus = Status.DEAD;
            return true;
        }
        Statistics["Hp"] = Hp;
        return false;
    }

    static public void Save(Entity e, string filename="save.json")
    {
        string jsonString = JsonSerializer.Serialize(e);
        File.WriteAllText(filename, jsonString);
    }

    public int Get(string statisticsName)
    {
        if (Statistics.TryGetValue(statisticsName, out int value))
            return value;
        else
            return 0;
    }

    public void Set(string statisticsName, int value)
    {
        Statistics[statisticsName] = value;
    }
}


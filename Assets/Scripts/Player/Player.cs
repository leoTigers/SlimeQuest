using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Player
{
    public Vector3 Position { get; set; }
    public string CurrentMap { get; set; }
    public Entity PlayerEntity { get; set; }
    public List<BaseItem> Inventory { get; set; }
    public KillStatistics Kills { get; set; }
    public List<SlimeType> Types { get; set; }

    public Player()
    {
        Inventory = new();
        Types = new();
    }

    public static Player Load(string filename)
    {
        Player player = null;
        if (File.Exists(filename))
        {
            JsonSerializer serializer = new();
            using StreamReader sr = new(filename);
            using (JsonReader reader = new JsonTextReader(sr))
            {
                player = (Player)serializer.Deserialize(reader, typeof(Player));
            }
        }
        else
        {
            List<BaseItem> l = new();
            l.Add(new Item.Wood(4));

            player = new()
            {
                Position = new Vector3(-4, 2.5f, -1),
                CurrentMap = "Start",
                PlayerEntity = new Entity(name: "Slime", hp: 69, hpMax: 69, mp: 25, mpMax: 25, physicalAttack: 35, physicalDefense: 1000, magicalAttack: 10, magicalDefense: 10),
                Inventory = l,
                Kills = new KillStatistics(),
                Types = new()
            };
        }
        return player;
    }

    public void AddLoot(List<BaseItem> items)
    {
        Inventory.AddRange(items);
        Inventory = BaseItem.Reduce(Inventory);
    }

    public void SaveState(string filename)
    {
        JsonSerializer serializer = new();
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        using StreamWriter sw = new(filename);
        using JsonWriter writer = new JsonTextWriter(sw);
        serializer.Serialize(writer, this);
    }

    public void SelectSprite()
    {
        string spritePath = "Sprites/slime_test";

        switch (Types.Count())
        {
            case 1:
                spritePath = Types[0] switch
                {
                    SlimeType.FIRE => "Sprites/fire_slime_v1",
                    SlimeType.WATER => "Sprites/water_slime",
                    SlimeType.PLANT => "Sprites/Green_slime",
                    _ => "Sprites/slime_test"
                };
                break;

            case 2:
                switch (Types[0])
                {
                    case SlimeType.FIRE:
                        spritePath = Types[1] switch
                        {
                            SlimeType.WATER => "Sprites/steam_slime",
                            SlimeType.PLANT => "Sprites/coal_slime_v2",
                            _ => "Sprites/slime_test"
                        };
                        break;

                    case SlimeType.WATER:
                        spritePath = Types[1] switch
                        {
                            SlimeType.FIRE => "Sprites/sky_slime",
                            SlimeType.PLANT => "Sprites/heal_slime_v1",
                            _ => "Sprites/slime_test"
                        };
                        break;

                    case SlimeType.PLANT:
                        spritePath = Types[1] switch
                        {
                            SlimeType.WATER => "Sprites/venom_slime",
                            SlimeType.FIRE => "Sprites/coal_slime_v2",
                            _ => "Sprites/slime_test"
                        };
                        break;
                }
                break;
            case 3:
                spritePath = "Sprites/karen_slime";
                break;
        }

        GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
    }

    public void AddType(SlimeType type)
    {
        Types.Add(type);

        SelectSprite();        
    }
}
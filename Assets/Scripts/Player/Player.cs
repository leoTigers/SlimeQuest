﻿using Newtonsoft.Json;
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
    public List<string> Types { get; set; }

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

    public void AddType(string type)
    {
        Types.Add(type);
        string spritePath = "";

        switch (Types.Count())
        {
            case 1:
                spritePath = Types[0] switch
                {
                    "Fire" => "Sprites/fire_slime_v1",
                    "Water" => "Sprites/water_slime",
                    "Plant" => "Sprites/Green_slime",
                    _ => "Sprites/slime_test"
                };
                break;

            case 2:
                switch (Types[0])
                {
                    case "Fire":
                        spritePath = Types[1] switch
                        {
                            "Water" => "Sprites/steam_slime",
                            _ => "Sprites/slime_test"
                        };
                        break;

                    case "Water":
                        spritePath = Types[1] switch
                        {
                            "Fire" => "Sprites/sky_slime",
                            "Plant" => "Sprites/heal_slime_v1",
                            _ => "Sprites/slime_test"
                        };
                        break;

                    case "Plant":
                        spritePath = Types[1] switch
                        {
                            "Water" => "Sprites/venom_slime",
                            _ => "Sprites/slime_test"
                        };
                        break;
                }
                break;
        }

        GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
    }
}
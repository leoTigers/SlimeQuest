﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class Player
{
    public Vector3 Position { get; set; }
    public string CurrentMap { get; set; }
    public Entity PlayerEntity { get; set; }
    public List<BaseItem> Inventory { get; set; }
    public KillStatistics Kills { get; set; }

    public Player()
    {

    }

    static public Player Load(string filename)
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
            List<BaseItem> l = new List<BaseItem>();
            l.Add(new Item.Wood(4));

            player = new()
            {
                Position = new Vector3(-4, 2.5f, -1),
                CurrentMap = "Start",
                PlayerEntity = new Entity(name: "Slime", hp: 69, hpMax: 69, mp: 25, mpMax: 25, physicalAttack: 35, physicalDefense: 1000, magicalAttack: 10, magicalDefense: 10),
                Inventory = l,
                Kills = new KillStatistics()
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
}

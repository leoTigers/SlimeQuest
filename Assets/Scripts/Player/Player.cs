using System;
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
    public Entity entity { get; set; }
    public List<Item> Inventory { get; set; }
    public KillStatistics kills { get; set; }

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
            List<Item> l = new List<Item>();
            l.Add(new Item("Coffee", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 4));

            player = new()
            {
                Position = new Vector3(-4, 2.5f, -1),
                CurrentMap = "Start",
                entity = new Entity(name: "Slime", hp: 69, hpMax: 69, mp: 25, mpMax: 25, physicalAttack: 15, physicalDefense: 1000, magicalAttack: 10, magicalDefense: 10),
                Inventory = l,
                kills = new KillStatistics()
            };
        }
        return player;

        /*        XmlSerializer serializer = new XmlSerializer(typeof(Player));
                // Declare an object variable of the type to be deserialized.
                Player s;

                if(File.Exists(filename))
                {
                    using (Stream reader = new FileStream(filename, FileMode.Open))
                    {
                        // Call the Deserialize method to restore the object's state.
                        s = (Player)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    List<Item> l = new List<Item>();
                    l.Add(new Item("Coffee", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 4));

                    s = new()
                    {
                        Position = new Vector3(-4, 2.5f, -1),
                        CurrentMap = "Start",
                        entity = new Entity(name: "Slime", hp: 69, hpMax: 69, mp: 25, mpMax: 25, physicalAttack: 15, physicalDefense: 1000, magicalAttack: 10, magicalDefense: 10),
                        Inventory = l
                    };
                };
                return s;*/
    }

    public void SaveState(string filename)
    {
        JsonSerializer serializer = new();
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        using StreamWriter sw = new(filename);
        using JsonWriter writer = new JsonTextWriter(sw);
        serializer.Serialize(writer, this);

        /*        XmlSerializer ser = new XmlSerializer(typeof(Player));
                TextWriter writer = new StreamWriter(filename);

                ser.Serialize(writer, this);
                writer.Close();
                Debug.Log("SAVED");*/
    }


}

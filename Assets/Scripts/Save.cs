using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity;
using UnityEngine;

[Serializable]
public class Save
{
    public Vector3 Position { get; set; }
    public string CurrentMap { get; set; }
    public Entity e { get; set; }
    public List<Item> Inventory { get; set; }

    public Save()
    {

    }

    public Save Load(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Save));

        // Declare an object variable of the type to be deserialized.
        Save s;

        if(File.Exists(filename))
        {
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                s = (Save)serializer.Deserialize(reader);
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
                e = new Entity(name: "Slime", hp: 69, hpMax: 69, mp: 25, mpMax: 25, physicalAttack: 15, physicalDefense: 1000, magicalAttack: 10, magicalDefense: 10),
                Inventory = l
            };
        };
        return s;
    }

    public void SaveState(string filename)
    {
        XmlSerializer ser = new XmlSerializer(typeof(Save));
        TextWriter writer = new StreamWriter(filename);

        ser.Serialize(writer, this);
        writer.Close();
        Debug.Log("SAVED");
    }
}

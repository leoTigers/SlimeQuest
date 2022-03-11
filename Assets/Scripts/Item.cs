using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Item 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public uint Count { get; set; }

    public Item()
    {
        Name = "";
        Description = "";
        Count = 0;
    }
    public Item(string name, string description, uint count)
    {
        Name = name;
        Description = description;
        Count = count;
    }
}

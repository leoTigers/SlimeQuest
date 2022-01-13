using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

public class Save
{
    public Vector3 Position { get; set; }
    public string CurrentMap { get; set; }

    public Save Load()
    {
        Save s = new()
        {
            Position = new Vector3(-4, 2.5f, -1),
            CurrentMap = "Start"
        };
        return s;
    }

    public void SaveState()
    {

    }
}

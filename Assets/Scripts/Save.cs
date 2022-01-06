using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

public class Save
{
    Vector3 position;
    string currentMap;

    public Save Load()
    {
        Save s = new Save();
        s.position = new Vector3(-4, 2.5f, -1);
        s.currentMap = "Start";
        return s;
    }

    public void SaveState()
    {

    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public string GetCurrentMap()
    {
        return currentMap;
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity;
using Newtonsoft.Json;

[Serializable]
public class KillStatistics
{
    public Dictionary<string, int> killCount;

    public KillStatistics()
    {
        killCount = new Dictionary<string, int>();
    }

    public int GetKillCount(string enemyName)
    {
        if (killCount.TryGetValue(enemyName, out int value))
            return value;
        return 0;
    }

    public void AddKillCount(string enemyName, int count=1)
    {
        if (killCount.TryGetValue(enemyName, out int value))
            killCount[enemyName] = value + count;
        else
           killCount[enemyName] = 1;
    }

    static public KillStatistics FromJson(string json)
    {
        return (KillStatistics)JsonConvert.DeserializeObject(json);
    }
    
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

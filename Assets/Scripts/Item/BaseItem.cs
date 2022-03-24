using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public class BaseItem
{
    public const int RARITY_COMMON = 0;
    public const int RARITY_UNCOMMON = 1;
    public const int RARITY_RARE = 2;
    public const int RARITY_EXOTIC = 3;
    public const int RARITY_LEGENDARY = 4;


    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public int Rarity { get; set; }
    public int Value { get; set; }


    public BaseItem(string name, string description, int count, int value, int rarity = BaseItem.RARITY_COMMON)
    {
        Name = name;
        Description = description;
        Count = count;
        Value = value;
        Rarity = rarity;
    }
    public BaseItem()
    {
        Name = "";
        Description = "";
        Count = 0;
        Rarity = RARITY_COMMON;
        Value = 0;
    }

    static public List<BaseItem> Reduce(IEnumerable<BaseItem> items)
    {
        List<BaseItem> newItems = new List<BaseItem>();
        foreach (BaseItem item in items)
        {
            int index;
            if ((index = newItems.FindIndex(x => x.Name == item.Name)) >= 0)
                newItems[index].Count += item.Count;
            else
                newItems.Add(item);
        }
        return newItems;
    }
}

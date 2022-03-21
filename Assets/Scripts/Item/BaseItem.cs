using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BaseItem
{
    public const int RARITY_COMMON = 0;
    public const int RARITY_UNCOMMON = 1;
    public const int RARITY_RARE = 2;
    public const int RARITY_EXOTIC = 3;
    public const int RARITY_LEGENDARY = 4;


    public string Name { get; set; }
    public string Description { get; set; }
    public uint Count { get; set; }
    public int Rarity { get; set; }
    public int Value { get; set; }


    public BaseItem(string name, string description, uint count, int value, int rarity = BaseItem.RARITY_COMMON)
    {
        Name = name;
        Description = description;
        Count = count;
        Value = value;
        Rarity = rarity;
    }
    public BaseItem()
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseEnemy: Entity
{
    public List<Tuple<float, Type, int, int>> LootTable;
    public int XpValue;

    public BaseEnemy(string spriteLocation = "", string name = "Unknown", int hpMax = 100, int mpMax = 25, int physicalAttack = 5, int physicalDefense = 5,
    int magicalAttack = 5, int magicalDefense = 5, int xpValue = 5): 
        base(name: name, spriteLocation: spriteLocation, hpMax: hpMax, mpMax: mpMax,
            physicalAttack: physicalAttack, physicalDefense: physicalDefense,
            magicalAttack: magicalAttack, magicalDefense: magicalDefense)
    {
        XpValue = xpValue;
        LootTable = new List<Tuple<float, Type, int, int>>();
    }

    public void AddLoot(float dropRate, Type item, int minAmount=1, int maxAmount=1)
    {
        if (item.Namespace == "Item")
            LootTable.Add(new Tuple<float, Type, int, int>(dropRate, item, minAmount, maxAmount));
    }

    public List<BaseItem> Loot(float luckFactor = 1)
    {
        List<BaseItem> loot = new List<BaseItem>();
        Random r = new();

        foreach(Tuple<float, Type, int, int> tableEntry in LootTable)
        {
            for (int i = tableEntry.Item3 - 1; i < tableEntry.Item4; i++)
            {
                if (r.NextDouble() < tableEntry.Item1)
                {
                    BaseItem it = (BaseItem)Activator.CreateInstance(tableEntry.Item2);
                    it.Count = 1;
                    loot.Add(it);
                }
            }
        }
        return loot;
    }
}

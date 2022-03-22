using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item
{
    public class SpiritOfFighting : BaseItem
    {
        public SpiritOfFighting(int count):
            base(name: "Spirit of fighting", description: "You managed to harvest this spirit from the worthiest ennemies",
                count: count, value: 1, rarity: RARITY_LEGENDARY)
        { }

        public SpiritOfFighting(): this(1) { }
    }
}

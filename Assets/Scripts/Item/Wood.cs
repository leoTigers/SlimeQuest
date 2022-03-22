using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item
{
    public class Wood: BaseItem
    {
        public Wood(int count):
            base(name: "Wood", description: "A piece of wood",
                count: count, value: 1, rarity: RARITY_COMMON)
        { }

        public Wood(): this(1) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item
{
    public class WaterMateria: BaseItem
    {
        public WaterMateria(int count):
            base(name: "Water materia", description: "The rare essence of water",
                count: count, value: 1, rarity: RARITY_RARE)
        { }

        public WaterMateria(): this(1) { }
    }
}

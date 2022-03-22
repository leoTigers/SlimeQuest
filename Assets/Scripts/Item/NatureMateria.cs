using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item
{
    public class NatureMateria: BaseItem
    {
        public NatureMateria(int count):
            base(name: "Nature materia", description: "The rare essence of nature",
                count: count, value: 1, rarity: RARITY_RARE)
        { }

        public NatureMateria(): this(1) { }
    }
}

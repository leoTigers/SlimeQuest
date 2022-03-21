using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy
{
    public class Lion : BaseEnemy
    {
        public Lion(int level): base(name: "Lion", spriteLocation: "Sprites/lion_enemy",
            hpMax: 20 + level * 3, mpMax: 10 + level * 2,
            physicalAttack: 5 + level, physicalDefense: 3 + 2 * level,
            magicalAttack: 3 + level, magicalDefense: 3 + level,
            xpValue: 5 + 5 * level)
        {
        }
    }
}

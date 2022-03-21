using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy
{
    public class Meduse : BaseEnemy
    {
        public Meduse(int level): base(name: "Meduse", spriteLocation: "Sprites/meduse_enemy",
            hpMax: 20 + level * 3, mpMax: 10 + level * 2,
            physicalAttack: 5 + level, physicalDefense: 3 + 2 * level,
            magicalAttack: 3 + level, magicalDefense: 3 + level,
            xpValue: 5 + 5 * level)
        {
        }
    }
}

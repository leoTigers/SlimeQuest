using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum Status
{
    NONE,
    POISONED,
    PARALIZED,
    CONFUSED,
    BURNED
}

public struct FightingEntity
{
    public FightingEntity(string _name, int _hp, int _hpMax, int _mp, int _mpMax, Status _status)
    {
        Name = _name;
        Hp = _hp;
        HpMax = _hpMax;
        Mp = _mp;
        MpMax = _mpMax;
        EntityStatus = _status;
        Sp = null;
    }
    public FightingEntity(string _name, int _hp, int _hpMax, int _mp, int _mpMax, Status _status, string _sp)
    {
        Name = _name;
        Hp = _hp;
        HpMax = _hpMax;
        Mp = _mp;
        MpMax = _mpMax;
        EntityStatus = _status;
        Sp = _sp;
    }

    public string Name { get; set; }
    public int Hp { get; set; }
    public int HpMax { get; set; }
    public int Mp { get; set; }
    public int MpMax { get; set; }
    public Status EntityStatus { get; set; }
    public string Sp { get; set; }
}
public class FightEntity
{


}

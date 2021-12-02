using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics
{

    public int hp; // health point
    public int mp; // mana point

    public int physicalDmg;
    public int magicalDmg;
    public int physicalDefense;
    public int magicalDefense;
    public int speed;
    public int luck;
    public string element;
    public CharacterStatistics(int m_hp, int m_mp, int m_physicalDmg, int m_magicalDmg, int m_physicalDefense, int m_magicalDefense, int m_speed, int m_luck, string m_element)

    {
        hp = m_hp;
        mp = m_mp;
        physicalDmg = m_physicalDmg;
        magicalDmg = m_magicalDmg;
        physicalDefense = m_physicalDefense;
        magicalDefense = m_magicalDefense;
        speed = m_speed;
        luck = m_luck;
        element = m_element;
    }
}

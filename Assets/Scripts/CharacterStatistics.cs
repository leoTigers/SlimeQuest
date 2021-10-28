using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new character")]
public class CharacterStatistics : ScriptableObject
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
}

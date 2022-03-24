using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEntryBehavior : MonoBehaviour
{
    public TMPro.TextMeshProUGUI LootName;
    public TMPro.TextMeshProUGUI LootCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(string name, int count,
        int rarity = BaseItem.RARITY_COMMON,
        string? color = null, int? countOver = null)
    {
        if(color == null)
        {
            color = "<color=";
            color += rarity switch
            {
                BaseItem.RARITY_COMMON => "grey",
                BaseItem.RARITY_UNCOMMON => "blue",
                BaseItem.RARITY_RARE => "green",
                BaseItem.RARITY_EXOTIC => "orange",
                BaseItem.RARITY_LEGENDARY => "purple",
                _ => "white",
            };
        }else
        {
            color = $"<color={color}";
        }
        LootName.text = $"{color}>{name}</color>";
        LootCount.text = countOver == null? $"{count}" : $"{count} / {countOver}";
    }

}

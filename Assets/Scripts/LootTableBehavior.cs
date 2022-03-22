using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTableBehavior : MonoBehaviour
{
    public GameObject lootEntryPrefab;
    public GameObject table;
    public TMPro.TextMeshProUGUI xpText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLoot(IEnumerable<BaseItem> loot)
    {
        foreach(BaseItem item in loot)
        {
            GameObject go = Instantiate(lootEntryPrefab, table.transform);
            go.GetComponent<LootEntryBehavior>().SetItem(item.Name, item.Count, item.Rarity);
        }
    }

    public void DisplayXp(int value)
    {
        xpText.text = $"+ {value} xp";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTableBehavior : MonoBehaviour
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

    public void DisplayRequirements(List<string> names, List<int> currentAmount, List<int> requiredAmount)
    {
        for(int i = 0; i < names.Count; i++)
        {
            GameObject go = Instantiate(lootEntryPrefab, table.transform);
            go.GetComponent<LootEntryBehavior>().SetItem(names[i], currentAmount[i], color: currentAmount[i]>=requiredAmount[i]?"green":"red", countOver: requiredAmount[i]);
        }
    }

    public void DisplayTime(double counter)
    {
        xpText.text = $"{counter}";
    }

    public void DisplayXp(int value)
    {
        xpText.text = $"+ {value} xp";
    }
}

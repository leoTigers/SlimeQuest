using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Reflection;
using System;
using System.Linq;

[System.Serializable]
public class Condition
{
    public string ItemName;
    public int Amount;
    public Condition()
    {

    }
}

[Inspectable]
public class AltarBehavior : MonoBehaviour
{
    [Inspectable]
    public List<Condition> ItemRequirements;
    [Inspectable]
    public int XpCost;
    [Inspectable]
    public SlimeType TypeEarned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObj = collision.gameObject;
        if (collision.name != "Player")
            return;

        bool valid = true;
        // Check conditions
        foreach(Condition condition in ItemRequirements)
        {
            if (MapSceneManager.player.Inventory.FirstOrDefault<BaseItem>(x => 
                x.Name == condition.ItemName).Count <= condition.Amount)
            {
                valid = false;
            }
        }
        if(!valid)
        {
            // display missing item panel
            return;
        }

        if(MapSceneManager.player.PlayerEntity.Xp < XpCost)
        {
            // display missing xp panel
            return;
        }

        // Display asking panel
        bool playerInput = true; // = GetPlayerInput()

        if (!playerInput)
            return;

        // Evolution
        MapSceneManager.player.AddType(TypeEarned);
        
    }
}

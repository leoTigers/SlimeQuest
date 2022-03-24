using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Reflection;
using System;
using System.Linq;
using UnityEngine.UI;

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

    private float counter;
    private bool inTrigger;
    private bool evolutionTriggered;

    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        inTrigger = false;
        gradient = new();
        
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.green;
        colorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inTrigger)
            return;
        counter += Time.deltaTime;

        // update progressbar
        if (!evolutionTriggered)
        {
            Vector2 rescale = GameObject.Find("Progressbar").GetComponent<RectTransform>().sizeDelta;
            GameObject.Find("Progressbar").GetComponent<RectTransform>().sizeDelta = new Vector2(Math.Min(counter, 5) * 240.0f, rescale.y);
            GameObject.Find("Progressbar").GetComponent<Image>().color = gradient.Evaluate(counter / 5.0f);
        }
        //GameObject.Find("Player").GetComponentInChildren<ItemTableBehavior>().DisplayTime(counter);

        if (counter > 5 && !evolutionTriggered)
        {
            Evolve();
        }
    }

    private void Evolve()
    {
        evolutionTriggered = true;
        MapSceneManager.player.AddType(TypeEarned);
        foreach(Condition condition in ItemRequirements)
        {
            int index = MapSceneManager.player.Inventory.FindIndex(x => x.Name == condition.ItemName);
            MapSceneManager.player.Inventory[index].Count -= condition.Amount;
        }
        GameObject.Find("Player").transform.Find("EvolutionRequirement").GetChild(0).gameObject.SetActive(false); // viewport
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
        evolutionTriggered = false;
        counter = 0;
        for(int i = 0; i < GameObject.Find("Player").transform.Find("EvolutionRequirement").GetChild(0).GetChild(1).childCount; i++)
        {
            Destroy(GameObject.Find("Player").transform.Find("EvolutionRequirement").GetChild(0).GetChild(1).GetChild(i).gameObject);
        }
        GameObject.Find("Player").transform.Find("EvolutionRequirement").GetChild(0).gameObject.SetActive(false); // viewport
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObj = collision.gameObject;
        if (collision.name != "Player")
            return;


        List<string> names = ItemRequirements.Select(x => x.ItemName).ToList();
        List<int> itemReqAmount = ItemRequirements.Select(x => x.Amount).ToList();
        List<BaseItem> itemOwn = new();
        for (int i = 0; i < names.Count; i++)
            itemOwn.Add(MapSceneManager.player.Inventory.FirstOrDefault(y => y.Name == names[i]));
        var itemOwnAmount = itemOwn.Select(x => x!=null?x.Count:0).ToList();

        GameObject.Find("Player").transform.Find("EvolutionRequirement").GetChild(0).gameObject.SetActive(true); // viewport
        GameObject.Find("Player").GetComponentInChildren<ItemTableBehavior>().DisplayRequirements(names, itemOwnAmount, itemReqAmount);

        bool valid = true;
        // Check conditions
        for(int i = 0; i < names.Count; i++)
        {
            if (itemOwnAmount[i] < itemReqAmount[i])
            {
                valid = false;
            }
        }
        if(!valid)
        {
            // display missing item panel
            Debug.Log("Missing Requirement");
            return;
        }

        if(MapSceneManager.player.PlayerEntity.Xp < XpCost)
        {
            // display missing xp panel
            Debug.Log("Missing xp");
            return;
        }
        int index = MapSceneManager.player.Types.FindIndex(x => x == TypeEarned);
        if (index >= 0)
        {
            Debug.Log("Type already present");
            return;
        }

        inTrigger = true;
        // Display asking panel
        //bool playerInput = true; // = GetPlayerInput()

        //if (!playerInput)
        //    return;

        // Evolution
        //MapSceneManager.player.AddType(TypeEarned);

    }
}

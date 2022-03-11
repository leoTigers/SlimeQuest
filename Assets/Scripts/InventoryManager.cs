using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;

public class InventoryManager : MonoBehaviour
{
    private int selected;
    private List<GameObject> itemList;
    public GameObject ItemPrefab;
    public bool isActive;
    public TMPro.TextMeshProUGUI descriptionBox;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        selected = 0;
        itemList = new List<GameObject>();
        foreach(Item item in SceneLoaderScript.playerSave.Inventory)
        {
            Debug.Log(item);
            GameObject go = Instantiate(ItemPrefab, GameObject.Find("ItemList").transform);
            go.transform.Find("SelectedIcon").gameObject.SetActive(false);
            go.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Name;
            go.transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text = item.Count.ToString();
            itemList.Add(go);
        }
        itemList[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        //yield return new WaitForSecondsRealtime(0.06f);
        itemList[selected].GetComponent<SelectableBehavior>().isSelected = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selected -= 1;
            if (selected == -1)
                selected = itemList.Count - 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selected = (selected + 1) % itemList.Count;
        }
        itemList[selected].GetComponent<SelectableBehavior>().isSelected = true;
        descriptionBox.text = SceneLoaderScript.playerSave.Inventory[selected].Description;

    }
    public void SetActive(bool active)
    {
        isActive = active;
    }
}

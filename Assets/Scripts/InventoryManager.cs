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
    public TMPro.TextMeshProUGUI descriptionBox;

    // Start is called before the first frame update
    void Awake()
    {
        selected = 0;
        itemList = new List<GameObject>();
        /*for (int i = 0; i < GameObject.Find("ItemList").transform.childCount; i++)
            Destroy(GameObject.Find("ItemList").transform.GetChild(i).gameObject);

        foreach (BaseItem item in SceneLoaderScript.playerSave.Inventory)
        {
            GameObject go = Instantiate(ItemPrefab, GameObject.Find("ItemList").transform);
            go.transform.Find("SelectedIcon").gameObject.SetActive(false);
            go.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Name;
            go.transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text = item.Count.ToString();
            itemList.Add(go);
        }
        itemList[0].SetActive(true);*/
    }

    void OnEnable()
    {
        selected = 0;
        foreach (GameObject go in itemList)
            Destroy(go);
        itemList.Clear();

        foreach (BaseItem item in SceneLoaderScript.playerSave.Inventory)
        {
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
}

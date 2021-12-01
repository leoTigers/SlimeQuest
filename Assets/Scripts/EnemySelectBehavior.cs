using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySelectBehavior : MonoBehaviour
{
    public bool isActive;
    public GameObject EnemyListComponent;
    public GameObject cursor;
    public GameObject enemyNameBox;
    public Text enemyNameTxt;
    private int enemyCount;
    private int selected;
    private bool frameLock;
    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameLock)
        {
            frameLock = false;

            selected = 0;
            while (FightManager.enemies[selected].hp == 0)
                selected++;
            return;
        }
        if (!isActive)
            return;

        enemyCount = EnemyListComponent.transform.childCount;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int originalSelection = selected;
            do
            {
                selected = (selected + 1) % enemyCount;
            } while (selected != originalSelection && FightManager.enemies[selected].hp == 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int originalSelection = selected;
            do
            {
                selected--;
                if (selected < 0)
                    selected = enemyCount - 1;
            } while (selected != originalSelection && FightManager.enemies[selected].hp == 0);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Selected: " + selected);
            FindObjectOfType<FightManager>().Attack(FightManager.player, selected);
            FightManager.playerInMenu = false;
            SetActive(false);
            //isActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
            FindObjectOfType<VerticalNavigationMenuBehavior>().SetActive(true);
        }
        cursor.transform.position = EnemyListComponent.transform.GetChild(selected).transform.position;
        enemyNameTxt.text = FightManager.enemies[selected].name;
    }

    public void SetActive(bool active)
    {
        isActive = active;
        cursor.SetActive(active);
        enemyNameBox.SetActive(active);
        frameLock = true;
    }
}

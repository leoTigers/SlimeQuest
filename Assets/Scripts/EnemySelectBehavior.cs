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
    public int attackType = 0;
    private int enemyCount;
    private int selected;
    private bool frameLock;
    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
        attackType = 0;
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
            StartCoroutine(PlayerAttack());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
            FindObjectOfType<VerticalNavigationMenuBehavior>().SetActive(true);
        }
        cursor.transform.position = EnemyListComponent.transform.GetChild(selected).transform.position;
        enemyNameTxt.text = FightManager.enemies[selected].name;
    }

    IEnumerator PlayerAttack()
    {
        SetActive(false);
        if(attackType == 0)
            yield return FindObjectOfType<FightManager>().Attack(FightManager.player, selected);
        if(attackType == 1)
        {
            if (FightManager.player.mp < 5)
            {
                yield return FindObjectOfType<FightManager>().Warning("Not enough mana !");
                SetActive(true);
            }
            else
            {
                yield return FindObjectOfType<FightManager>().MagicAttack(FightManager.player, selected);
                FightManager.playerInMenu = false;
            }
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
        cursor.SetActive(active);
        enemyNameBox.SetActive(active);
        frameLock = true;
    }
}

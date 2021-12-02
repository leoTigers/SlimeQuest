using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalNavigationMenuBehavior : MonoBehaviour
{

    public int selected;
    public bool isActive;
    private int childCount;
    private List<GameObject> childrens;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        selected = 0;
        childCount = transform.childCount;
        childrens = new List<GameObject>();
        
        for(int i = 0; i < childCount; i++) 
            childrens.Add(transform.GetChild(i).gameObject);
        childrens[selected].GetComponent<SelectableBehavior>().isSelected = true;
       // StartCoroutine(PlayerMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        //yield return new WaitForSecondsRealtime(0.06f);
        childrens[selected].GetComponent<SelectableBehavior>().isSelected = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selected -= 1;
            if (selected == -1)
                selected = childCount - 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selected = (selected + 1) % childCount;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SetActive(false);
            switch (selected)
            {
                case 0:
                    {
                        EnemySelectBehavior esb = FindObjectOfType<FightManager>().GetComponent<EnemySelectBehavior>();
                        esb.attackType = 0;
                        esb.SetActive(true);
                    }
                    break;
                case 1:
                    {
                        EnemySelectBehavior esb = FindObjectOfType<FightManager>().GetComponent<EnemySelectBehavior>();
                        esb.attackType = 1;
                        esb.SetActive(true);
                    }
                    break;
                case 2:
                    StartCoroutine(PlayerHeal());
                    break;
                case 3:
                    FindObjectOfType<FightManager>().Defend();
                    FightManager.playerInMenu = false;
                    break;
                default:
                    SetActive(true);
                    break;
            }
        }

        childrens[selected].GetComponent<SelectableBehavior>().isSelected = true;
    }

    IEnumerator PlayerHeal()
    {
        if (FightManager.player.mp < 4)
        {
            yield return FindObjectOfType<FightManager>().Warning("Not enough mana !");
            SetActive(true);
        }
        else
        {
            yield return FindObjectOfType<FightManager>().Heal(FightManager.player);
            FightManager.playerInMenu = false;
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}

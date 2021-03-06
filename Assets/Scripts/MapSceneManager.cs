using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour
{
    static public Player player;
    public GameObject sceneToDisable;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        player = SceneLoaderScript.playerSave; //new Entity(name:"Slime", hp:69, hpMax:69, mp:25, mpMax:25, physicalAttack:15, physicalDefense:1000, magicalAttack:10, magicalDefense:10);

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSceneActive(bool active)
    {
        isActive = active;
        sceneToDisable.SetActive(active);
    }

    public void StartFight(GameObject enemy)
    {
        //Destroy(enemy);
        enemy.transform.position += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        SetSceneActive(false);
        GameObject player = GameObject.Find("Player");
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerBehaviour>().enabled = false;
        foreach(MenuManager m in player.GetComponents<MenuManager>())
        {
            m.enabled = false;
        }
        SceneManager.LoadSceneAsync("Fight", LoadSceneMode.Additive);
    }
}

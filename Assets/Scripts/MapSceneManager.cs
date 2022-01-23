using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour
{
    static public Entity player;
    public GameObject sceneToDisable;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        player = SceneLoaderScript.playerSave.e; //new Entity(name:"Slime", hp:69, hpMax:69, mp:25, mpMax:25, physicalAttack:15, physicalDefense:1000, magicalAttack:10, magicalDefense:10);

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
        SceneManager.LoadSceneAsync("Fight", LoadSceneMode.Additive);
    }
}

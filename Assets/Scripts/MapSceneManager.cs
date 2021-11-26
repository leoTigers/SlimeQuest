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
        player = new Entity("Slime", 69, 25, 15, 1000, 10, 10, 10, 10, 1, "");
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

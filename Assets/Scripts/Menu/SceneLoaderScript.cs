using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    static public Player playerSave;

    public void Play()
    {
        playerSave = Player.Load("s1.json");
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = playerSave.Position;
        player.transform.GetChild(0).gameObject.SetActive(true); // camera
        player.transform.GetChild(1).gameObject.SetActive(true); // exit UI
        player.transform.GetChild(2).gameObject.SetActive(true); // option manager
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerBehaviour>().enabled = true;
        SceneManager.LoadSceneAsync(playerSave.CurrentMap);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

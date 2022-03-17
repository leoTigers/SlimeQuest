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
        SceneManager.LoadSceneAsync(playerSave.CurrentMap);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    static public Save playerSave;
    public void Play()
    {
        playerSave = new Save().Load();
        SceneManager.LoadSceneAsync(playerSave.GetCurrentMap());
    }

    public void Exit()
    {
        Application.Quit();
    }
}

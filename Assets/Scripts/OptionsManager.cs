using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    static public void saveGame()
    {
        SceneLoaderScript.playerSave.SaveState();
    }
}

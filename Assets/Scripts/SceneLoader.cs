using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public GameObject scene;
    public GameObject playerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(playerPrefab, SceneLoaderScript.playerSave.Position,
            Quaternion.identity);
        go.name = "Player";
        go.transform.parent = scene.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

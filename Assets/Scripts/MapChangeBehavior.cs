using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangeBehavior : MonoBehaviour
{
    public string nextMap;
    public Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject otherObj = col.gameObject;
        Debug.Log("Collided with: " + otherObj);
        if (otherObj.name == "Player")
        {
            otherObj.transform.position = nextPosition;
            MapSceneManager.player.CurrentMap = nextMap;
            SceneManager.LoadScene(nextMap);
        }
    }
}

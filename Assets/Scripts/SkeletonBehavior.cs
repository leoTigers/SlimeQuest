using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonBehavior : MonoBehaviour
{
    public GameObject sceneToDisable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        Debug.Log("Collided with: " + otherObj);
        if(otherObj.name == "Player")
        {
            sceneToDisable.SetActive(false);
            SceneManager.LoadSceneAsync("Fight", LoadSceneMode.Additive);

        }
    }
}

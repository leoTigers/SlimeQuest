using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject viewport;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        viewport.SetActive(!active);
        //menu = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            viewport.SetActive(!active);
            active = !active;
        }
    }

}

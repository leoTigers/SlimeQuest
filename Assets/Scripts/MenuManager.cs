using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject viewport;
    public bool active;
    public KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        viewport.SetActive(false);
        //menu = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && (PlayerBehaviour.inMenu == null || PlayerBehaviour.inMenu == key))
        {
            active = PlayerBehaviour.inMenu == null ? true : !active;
            viewport.SetActive(active);
            PlayerBehaviour.inMenu = active ? key : null;
        }
    }

}

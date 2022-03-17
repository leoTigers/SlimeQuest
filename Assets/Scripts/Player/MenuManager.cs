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
<<<<<<< Updated upstream:Assets/Scripts/MenuManager.cs
        if (Input.GetKeyDown(KeyCode.P))
=======
        if ((Input.GetKeyDown(key) && (PlayerBehaviour.inMenu == null || PlayerBehaviour.inMenu == key)))
>>>>>>> Stashed changes:Assets/Scripts/Player/MenuManager.cs
        {
            viewport.SetActive(!active);
            active = !active;
        }
    }
}

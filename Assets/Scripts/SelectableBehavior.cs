using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBehavior : MonoBehaviour
{

    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tf = this.transform.Find("SelectedIcon");
        tf.gameObject.SetActive(isSelected);
    }

    void SetSelected(bool state)
    {
        isSelected = state;
    }
}

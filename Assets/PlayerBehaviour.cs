using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //
    private float movementSpeed = 10f;

    // Animation
    private int animLength = 50;
    private float animScale = 0.99f;
    private int animFrame;

    // Start is called before the first frame update
    void Start()
    {
        animFrame = 0;

        Vector2 v = GetComponent<Renderer>().bounds.size;
        Debug.Log(v);
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
       
        //animate the sprite
        animate();

        //output to log the position change
        //Debug.Log(transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        Debug.Log("Collided with: " + otherObj);
    }

    void animate()
    {
        Vector2 rescale = transform.localScale;
        if(animFrame < animLength/2)
            transform.localScale = new Vector2(rescale.x, (float) (rescale.y * animScale));
        else
            transform.localScale = new Vector2(rescale.x, (float)(rescale.y / animScale));
        animFrame = (animFrame + 1) % animLength;
    }

}

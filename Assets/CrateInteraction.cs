using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrateInteraction : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public TextMeshPro crateText;
    public int boxesLeft;

    // Start is called before the first frame update
    void Start()
    {
        //identify what is a crate
        //find every instance
        //turn into int

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Get player collision
    //Check if player is dashing
    //Break box
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.collider.name == "PLAYER")
        {
            Debug.Log("I collided with " + collision.collider.name);

            var player = collision.gameObject;

            Destroy(gameObject);


        }
    }

}

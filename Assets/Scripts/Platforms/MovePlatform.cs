using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

   
    public bool inverted;           //Moves upside down that the player
    public Transform Area;          //Area of action for the platform
    private GameObject target;      //Target to copy (Player)
  
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }


    void FixedUpdate()
    {
        //Reads the trigger to know if player is in the Area
        // Reads the target velocity and move the platform with the same vel
        // Bool inverted decide if platform moves like the target or oposite direction
        if (Area.GetComponent<PlatformTrigger>().Active) {
            if (!inverted) rb.velocity = new Vector2(target.GetComponent<Rigidbody2D>().velocity.x, 0);
            else rb.velocity = new Vector2(target.GetComponent<Rigidbody2D>().velocity.x * -1, 0);
        }
        //Stops the platform when player leaves the area
        else rb.velocity = new Vector2(0, 0);
    }
}
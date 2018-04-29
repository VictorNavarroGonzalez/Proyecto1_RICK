using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidePlatform : MonoBehaviour {

    //Distance to move
    public float distX;
    public float timeX;

    //Velocity
    public float distY;
    public float timeY;

    //Direction selector
    public bool horizontal;
    public bool vertical;

    //Trigger
    public bool active;
    public Transform Area;

    private bool needTrigger;
    private float moveX;
    private float moveY;
    private float velX;
    private float velY;
    private bool startUp;
    private bool goY;
    private bool backY;
    private bool backX;
    private bool goX;
    private bool startRight;
    private Rigidbody2D rb;


    // Use this for initialization
    void Awake () {

        rb = GetComponent<Rigidbody2D>();

        //Position
        moveX = transform.position.x + distX;
        moveY = transform.position.y + distY;

        //Velocity
        velX = distX / timeX;
        velY = distY / timeY;

        //Direction control
        backX = false;  
        backY = false;  
        goX = true;     
        goY = true;    

        //Detect if platform starts moves right or left
        if (distX > 0) startRight = true;
        else startRight = false;

        //Detect if platform starts moves up or down
        if (distY > 0) startUp = true;
        else startUp = false;

        if (!active) needTrigger = true;
        else needTrigger = false;

        //Debug.Log(needTrigger);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Reads the trigger to know if player is in the Area
        if (active) {      
            CheckDirection();

            //Detects if platform has Horizontal movement
            if (horizontal) {
                if (goX) rb.velocity = new Vector2(velX, rb.velocity.y);            //Goes to select position
                else if (backX) rb.velocity = new Vector2(-velX, rb.velocity.y);    //Returns to the start position
            }

            //Detects if platform has Vertical movement
            if (vertical) {
                if (goY) rb.velocity = new Vector2(rb.velocity.x, velY);            //Goes to select position
                else if (backY) rb.velocity = new Vector2(rb.velocity.x, -velY);    //Returns to the start position
            }
        }
        //Detect if platform has activator or not
        //Stops the platform when player leaves the area
        else {
                active = Area.GetComponent<PlatformTrigger>().Active;
                rb.velocity = new Vector2(0, 0);
        }
    }

    void CheckDirection() {
        //Check Horizontal movement (if there is)
        if (horizontal) {   
            //If X axis is positive
            if (startRight) {
                if (transform.position.x < moveX && !backX) goX = true;     //Go right
                else goX = false;

                if (transform.position.x < moveX - distX) backX = false;    //Go left
                else if (!goX) backX = true;
            }
            //If X axis is negative
            else {
                if (transform.position.x > moveX && !backX) goX = true;     //Go left
                else goX = false;

                if (transform.position.x > moveX - distX) backX = false;    //Go right
                else if (!goX) backX = true;    
            }
        }

        //Check Vertical movement (if there is)
        if (vertical) {
            //If Y axis is positive
            if (startUp) {
                if (transform.position.y < moveY && !backY) goY = true;     //Go right
                else goY = false;

                if (transform.position.y < moveY - distY) backY = false;    //Go left
                else if (!goY) backY = true;
            }
            //If Y axis is negative
            else {
                if (transform.position.y > moveY && !backY) goY = true;     //Go left
                else goY = false;

                if (transform.position.y > moveY - distY) backY = false;    //Go right
                else if (!goY) backY = true;
            }
        }       
    }
}

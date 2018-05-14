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
    public Transform Area;

    private bool isReading;
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
    private bool isOnPlatform;
    private bool extraSpeed;
    private Rigidbody2D rb;
    private GameObject target;      //Target to copy (Player)

    // Use this for initialization
    void Awake () {

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");

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

        extraSpeed = false;
        isReading = false;

        //Debug.Log(needTrigger);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Reads the trigger to know if player is in the Area
        if (Area.GetComponent<PlatformTrigger>().Active) {      
            CheckDirection();

            //Detects if platform has Horizontal movement
            if (horizontal) {
                if (goX) rb.velocity = new Vector2(velX, rb.velocity.y);            //Goes to select position

                else if (backX) rb.velocity = new Vector2(-velX, rb.velocity.y);    //Returns to the start position

                //Move the Player with the platform
                if (isOnPlatform) {
                    if (!extraSpeed) {
                        extraSpeed = true;
                        target.GetComponent<PlayerMovement>().MaxSpeed = 10f + Mathf.Abs(velX);   
                    }
                    StartCoroutine(Soften());               //Prevent errors when platform change direction
                    if (Mathf.Abs(target.GetComponent<Rigidbody2D>().velocity.x) < (Mathf.Abs(rb.velocity.x) + 10f * Mathf.Abs(InputManager.MainHorizontal()))) {
                        target.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * 15, ForceMode2D.Force);
                    }
                }
                else if (extraSpeed) {
                    extraSpeed = false;
                    target.GetComponent<PlayerMovement>().MaxSpeed = 10;                   
                }
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

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && target.transform.position.y > this.transform.position.y) isOnPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") isOnPlatform = false;
    }

    public IEnumerator Soften() {
        if (!isReading) {
            isReading = true;
            bool temp = goX;
            yield return new WaitUntil(() => goX != temp);
            if (isOnPlatform) {
                target.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                target.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 15, ForceMode2D.Force);
            } 
            isReading = false;
        }       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidePlatform : MonoBehaviour {

    public float distX;
    public float timeX;
    public float distY;
    public float timeY;

    public bool hasTrigger;
    public bool horizontal;
    public bool vertical;

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

        moveX = transform.position.x + distX;
        moveY = transform.position.y + distY;

        velX = distX / timeX;
        velY = distY / timeY;

        backX = false;
        backY = false;

        goX = true;
        goY = true;

        if (distX > 0) startRight = true;
        else startRight = false;
        if (distY > 0) startUp = true;
        else startUp = false;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!hasTrigger) {
            CheckDirection();

            if (horizontal) {
                if (goX) rb.velocity = new Vector2(velX, rb.velocity.y);
                else if (backX) rb.velocity = new Vector2(-velX, rb.velocity.y);
            }

            if (vertical) {
                if (goY) rb.velocity = new Vector2(rb.velocity.x, velY);
                else if (backY) rb.velocity = new Vector2(rb.velocity.x, -velY);
            }
            
        }
        
    }

    void CheckDirection()
    {
        if (horizontal) {
            if (startRight) {
                if (transform.position.x < moveX && !backX) goX = true;
                else goX = false;

                if (transform.position.x < moveX - distX) backX = false;
                else if (!goX) backX = true;
            }
            else {
                if (transform.position.x > moveX && !backX) goX = true;
                else goX = false;

                if (transform.position.x > moveX - distX) backX = false;
                else if (!goX) backX = true;
            }
        }

        if (vertical) {
            if (startUp) {
                if (transform.position.y < moveY && !backY) goY = true;
                else goY = false;

                if (transform.position.y < moveY - distY) backY = false;
                else if (!goY) backY = true;
            }
            else {
                if (transform.position.y > moveY && !backY) goY = true;
                else goY = false;

                if (this.transform.position.y > moveY - distY) backY = false;
                else if (!goY) backY = true;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") hasTrigger = false;
    }
}
